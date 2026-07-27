using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Initilization

    public float currentHealth, maxHealth, damageAmount;

    public static PlayerHealth instance; // Track Player Health across different scripts

    public HealthBar healthbar; // Pulls HealthBar.cs script contents

    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(currentHealth); // using function from HealthBar.cs script
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DealDamage()
    {
        currentHealth -= damageAmount; // Decrease health
        healthbar.SetHealth(currentHealth); // Set HealthBar slider based on Player's health
        if (currentHealth <= 0)
        {
            //player dies
            Debug.Log("Player is Dead");
        }

    }

}
