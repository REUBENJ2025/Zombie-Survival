using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Healthbar System
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(float health) // Pass health as Parameter as float data type
    {
        slider.maxValue = health;
        slider.value = health;

    }

    public void SetHealth(float health) // Pass health as Parameter as float data type
    {
        slider.value = health;
    }

}
