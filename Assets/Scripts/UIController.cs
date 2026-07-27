using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Initilization
    public Image damageEffect;
    public float damageAlpha = 0.3f;

    public float damageFadeSpeed;

    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (damageEffect.color.a != 0) // Check if screen is red
        {
            damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, Mathf.MoveTowards(damageEffect.color.a, 0f, damageFadeSpeed*Time.deltaTime)); // Fade colour out smooth using Mathf.MoveTowards()
        }
    }

    public void ShowDamage()
    {
        damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, damageAlpha); // Keep RGB but change Transparency of red image 
    }
}
