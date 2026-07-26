using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //Initilization 
    public Transform attackPoint;
    public float AttackRadius;
    public LayerMask playerLayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, AttackRadius, playerLayer); // Storing data about detection in array inside the variable hitEnemies 

        //Loops each time player hits enemy, iterate through the array of potential enemies and once detected, decrease player health
        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("Player is damaged");
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, AttackRadius); //Check if zombie has attacked player and hit player
    }
}