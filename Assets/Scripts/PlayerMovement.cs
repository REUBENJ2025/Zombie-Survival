using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Using float data type for movement speed and jump force to mak it smoother and more precise.
//Time.deltaTime is used to calculate how long the key has been held down for
public class PlayerMovement : MonoBehaviour
{
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charcontroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
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
        

    }
}
