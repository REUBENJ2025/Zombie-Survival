using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level1Quest : MonoBehaviour
{
    private int enemiesDefeated = 0;
    private bool isCompleted = false;
    public static Level1Quest instance;
    public int desiredEnemies;
    private bool Briefcasefound = false;

    public GameObject questScreen;

    public Text quest1Text, quest2Text;

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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            questScreen.SetActive(!questScreen.activeSelf); // Press Tab to open and close quest screen
        }
    }

    public void DefeatEnemey()
    {
        enemiesDefeated++;
        CheckQuestsCompletion();
        UpdateQuestStatus();

    }

    private void CheckQuestsCompletion() // Check that quest is completed 
    {
        if(Briefcasefound && enemiesDefeated >= desiredEnemies)
        {
            isCompleted = true; // Boolean used to Flag completed quests
            Debug.Log("Quest is completed");
        }
    }

    public void BriefCaseFound()
    {
        Briefcasefound = true;
        CheckQuestsCompletion(); // Call functions to create new quests 
        quest2Text.color = Color.green; // Change quest text to green when completed
    }

    private void UpdateQuestStatus()
    {
        if(enemiesDefeated >= desiredEnemies)
        {
            quest1Text.color = Color.green; // Change quest text to green when completed
        }
    }
}
