using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
    if (other.gameObject.tag == "Player")
    {
        if(Level1Quest.instance.IsCompleted()) // Check if quests are completed before allowing player to enter portal
        {
     // Load next sequnetial scene when player collides with the portal collider using scene index in Build Profiles
        int currentScene = SceneManager.GetActiveScene().buildIndex; // Store current scene in variable currentScene
        SceneManager.LoadScene(currentScene + 1);
        }
        else
            {
                Debug.Log("Not completed quests yet");
            }
    }
    }
}