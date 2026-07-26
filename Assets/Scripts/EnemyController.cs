using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Initilization 
    private  GameObject player;
    public float detectingRange;
    public float moveSpeed;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        //Using Tag system to play Animations 
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player"); // Tells Unity we are refering to player 
    }

    // Update is called once per frame
    void Update()
    {
        //Distance checker from zombie

        float distance = Vector3.Distance(transform.position, player.transform.position); // Check distance from one position to the player 
        Debug.Log(distance); // Output distance in console 

        if (distance < detectingRange)
        {
            anim.SetBool("inRange", true); // Set inRage to true to make zombie run 
            MoveTowards();
        }
        else
        {
            anim.SetBool("inRange", false); // If not in detecting range then zombie will not run 
        }


    }

    //Zombie chase
    void MoveTowards()
    {
        // transform.position is the zombie position and player.position is the player's position

        Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z); // Only use player x and z axis for reference to avoid floating for zombie 
        Vector3 direction = (playerPosition - transform.position).normalized;

        transform.position = Vector3.MoveTowards(transform.position,playerPosition,moveSpeed*Time.deltaTime); // FInding player and zombie position and moving zombie to player position 

        transform.LookAt(playerPosition); //Rotate zombie to always look at player when chasing



    }
}