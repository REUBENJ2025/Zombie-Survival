using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level1Quest : MonoBehaviour
{
    private int enemiesDefeated = 0;
    private bool isCompleted = false;
    public static Level1Quest instance;
    public int desiredEnemies;

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
        
    }

    public void DefeatEnemey()
    {
        enemiesDefeated++;
        CheckQuestsCompletion();

    }

    private void CheckQuestsCompletion()
    {
        if(enemiesDefeated >= desiredEnemies)
        {
            isCompleted = true;
            Debug.Log("Quest is completed");
        }
    }
}
