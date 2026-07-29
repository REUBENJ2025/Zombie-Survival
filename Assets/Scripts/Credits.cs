using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public GameObject creditsScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CreditsRun());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CreditsRun() // Show credits screen, coroutine
    {
        yield return new WaitForSeconds(0.5f);
        creditsScreen.SetActive(true);
        yield return new WaitForSeconds(15);
        SceneManager.LoadScene(0); // Return to main menu from Scene Manger
    }
}
