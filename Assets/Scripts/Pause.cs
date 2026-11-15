using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public bool isPaused;
    public GameObject pauseScreen;  

    public static Pause instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // Escape key to pause
        {
            PauseUnpause();
        }
    }


    public void PauseUnpause()
    {
        if (isPaused) // Check if paused is active 
        {
            isPaused = false; 
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }

        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void MenuButton()
    {
        SceneManager.LoadScene(0);

    }
}
