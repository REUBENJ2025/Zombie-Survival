//Import namespace libaries 

using System.Collections; //Used for IEnumerator and coroutine
using System.Collections.Generic; // Used for Lists and Dictionaries (Shown by <>)
using UnityEngine; // Standard Unity libary such as Vectors and Physics
using UnityEngine.AI; // Unity Libary for pathfinding and AI implimentation 
using UnityEngine.InputSystem; // User inputs for keyboard/controller
using UnityEngine.UI; //Import libary for text element adjustments

//Using float data type for movement speed and jump force to mak it smoother and more precise.
//Time.deltaTime is used to calculate how long the key has been held down for
public class PlayerMovement : MonoBehaviour
{

    //Initilization
    public float moveSpeed; // The speed at which the player moves (Standard speed)
    private CharacterController charcontroller;
    private float horizontalInput; // The horizontal input from the player
    private float verticalInput;
    private Vector3 moveDirection; // The direction in which the player is moving

    private Vector3 movement; // The movement vector that will be applied to the player (Combining movement with orientation)

    public float runSpeed; // The speed at which the player runs (Updated speed)
    private float currentMoveSpeed; // The current speed of the player (can be walk or run speed)
 
    public float jumpForce; // Variable for amount of force applied when plauyer jumps

    public float gravitymod;

    // Ground check variables
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isOnGround;

    // Camera Variables
    public Transform playerViewPoint;
    public float mouseSensitivity;
    private float verticalRotation;
    private Vector2 mouseInput;
    public bool invertLook;
    private Camera playerCamera;


    public GameObject hitEffectPrefab; //Impact effect

    public Animator gunAnim;

    public float timeBetweenShots;
    private float shootCounter;

    public GameObject muzzle;


    // ammo

    public int mag = 5; // amount of magazines
    public int ammo = 25;
    public int ammoAmount = 25; // amount of ammo in each mag

    public Text ammoText;
    public Text magText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        magText.text = mag.ToString(); //Casting to String 
        ammoText.text = ammo + "/" + ammoAmount;

        charcontroller = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen and invisible
    }

    // Update is called once per frame
    void Update()
    {   
        //Horizontal rotation
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity; // Input from player. Mouse X and Mouse Y are mapped to Unity Input Manager. Vector 2 Takes to parameters
        transform.rotation=Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z); // Rotate player based on mouse input. Quaternion.Euler is used to convert the rotation from degrees to radians to simplify the rotation. Quaternion is used to make rotation smoother

        //Vertical rotation
        verticalRotation += mouseInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f); // Max vertical rotation is 60 degrees up and down.

        if(invertLook)
        {
            playerViewPoint.rotation = Quaternion.Euler(verticalRotation, playerViewPoint.eulerAngles.y, playerViewPoint.eulerAngles.z); // Invert camera if InvertLook is true.
        }
        else
        {
            playerViewPoint.rotation = Quaternion.Euler(-verticalRotation, playerViewPoint.eulerAngles.y, playerViewPoint.eulerAngles.z); // Normal camera if InvertLook is false.
        }
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontalInput * moveSpeed, 0f, verticalInput * moveSpeed);
        

    if (Input.GetKey(KeyCode.LeftShift)) // Check if player is holding down Left Shift to run and then use runSpeed Property in Unity to update player's speed
        {
            currentMoveSpeed = runSpeed;

        }
        else
        {
            currentMoveSpeed = moveSpeed; // If not holding down Left Shift, use normal moveSpeed Property in Unity to update player's speed
        }

        float verticalVelocity = movement.y; // Store the current vertical velocity of the player (to maintain jump and gravity effects)
        movement = ((transform.forward * moveDirection.z) + (transform.right * moveDirection.x)).normalized * currentMoveSpeed; // Calculate the movement vector based on the player's input and speed (Combining movement with orientation)
        movement.y = verticalVelocity; // Restore the vertical velocity to the movement vecetor


        if (charcontroller.isGrounded)
        {
            movement.y = 0f; // Use isGround property of CharacterController to check if player is on the ground and reset vertical velocity to 0 when player is on the ground
        }

        // Raycast to check if player is on ground (Similar to radar on a car)
        isOnGround = Physics.Raycast(groundCheck.position, Vector3.down, 0.25f, groundLayer); // Use Raycast to check if player is on the ground by casting a ray downwards from the groundCheck position and checking if it hits the groundLayer within a distance of 0.25 

        if (Input.GetButtonDown("Jump") && isOnGround)  // Using GetButtonDown method uses the Input Manager built into Unity to check keybind set for jump
        {
            movement.y = jumpForce; // Apply jump force to y axis of movement vector when jump button is pressed
        }
        
        
        movement.y += (Physics.gravity.y * gravitymod * Time.deltaTime); // Change gravity to be more realistic for jump mechanic, ensure player returns to ground after jumping
        
        
        charcontroller.Move(movement *Time.deltaTime);

        gunAnim.SetFloat("Speed", new Vector3(moveDirection.x, 0f, moveDirection.z).magnitude); // Set the speed parameter of the gun animator based on the player's movement speed


        // Semi-automatic firing check that there is ammo left in the mag
        if (Input.GetMouseButtonDown(0) & ammo > 0 && !gunAnim.GetCurrentAnimatorStateInfo(0).IsName("Reload")) //Defensive programming (Prevent shooting during reload animation)
        {
            StartCoroutine(MuzzleFlah()); // Call MuzzleFlash Function
            Shoot();
            gunAnim.SetTrigger("Shoot");

            ammo --;
        magText.text = mag.ToString(); //Casting to String 
        ammoText.text = ammo + "/" + ammoAmount;
        }


        // Automatic firing check that there is ammo left in the mag
        if(Input.GetMouseButton(0) && ammo > 0 && !gunAnim.GetCurrentAnimatorStateInfo(0).IsName("Reload")) //Defensive programming (Prevent shooting during reload animation)
        {
            shootCounter -= Time.deltaTime;
            if(shootCounter <= 0)
            {
                StartCoroutine(MuzzleFlah()); // Call MuzzleFlash Function
                Shoot();
                gunAnim.SetTrigger("Shoot");
                
                ammo --; // Decrement ammo by 1
                magText.text = mag.ToString(); //Casting to String 
                ammoText.text = ammo + "/" + ammoAmount;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor when Escape key is pressed
        }
        else if (Cursor.lockState == CursorLockMode.None)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked; // Lock the cursor when left mouse button is pressed
            }
        }
    }

    void Reload() // Remove mag and then replace ammo with ammount in the mag
    {
        if (mag > 0)
        {
            mag--;
            gunAnim.SetTrigger("Reload");

            ammo = ammoAmount;

        }
        //Change to function later
        magText.text = mag.ToString(); //Casting to String  
        ammoText.text = ammo + "/" + ammoAmount;

    }
    
    IEnumerator MuzzleFlah() //Function for muzzle flash effect when shooting with a wait time of 0.02 seconds
    {
        muzzle.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        muzzle.SetActive(false);
    }


    private void Shoot() //Shoot function to determine if something has been hit by the raycast of the gun
    {
       Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); // Implement shooting logic here
       ray.origin = playerCamera.transform.position;
       
       if (Physics.Raycast(ray, out RaycastHit hit))
         {
                // gunAnim.SetTrigger("Shoot");
                Debug.Log("We shoot shot" + hit.collider.gameObject.name); // Log the name of the object that was hit by the raycast
                Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal)); //Instantiate creates an object for hit effects
         }
         shootCounter = timeBetweenShots;
    }


    private void OnTriggerEnter(Collider other) //Pick up magazine and update total magazine and destroy game object when picked up
    {
        if(other.gameObject.tag == "Mag")
        {
            mag += 2;
            magText.text = mag.ToString();
            Destroy(other.gameObject); //Destroys game objects
        }
    }


    private void LateUpdate() // Once Camera is initialized in void update, apply the results to the camera to ensure camera is updated after camera inputs.
    {
        playerCamera.transform.position = playerViewPoint.position;
        playerCamera.transform.rotation = playerViewPoint.rotation;
    }
}