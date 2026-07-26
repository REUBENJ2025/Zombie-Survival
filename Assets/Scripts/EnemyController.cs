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
        }
        else
        {
            anim.SetBool("inRange", false); // If not in detecting range then zombie will not run 
        }


    }
}