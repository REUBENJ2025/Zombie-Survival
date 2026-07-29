using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Manages transition between scenes

public class Mainmenu : MonoBehaviour
{

    public string sceneName;
    public string creditsname;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() // Connect button to function for player to create a new game 
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LeaveGame() // Connect button to function for player to leave game
    {
        Application.Quit();
        Debug.Log("Player left game");
    }

    public void Credits()
    {
        SceneManager.LoadScene(creditsname); // Connect button to function for player to view game credits
    }

}
