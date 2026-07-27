using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Initilization

    public float currentHealth, maxHealth, damageAmount;

    public static PlayerHealth instance; // Track Player Health across different scripts


    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DealDamage()
    {
        currentHealth -= damageAmount; // Decrease health
        if (currentHealth <= 0)
        {
            //player dies
            Debug.Log("Player is Dead");
        }

    }

}
