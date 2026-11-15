using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRIPT : MonoBehaviour
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
            Level1Quest.instance.BriefCaseFound();
            Debug.Log("Briefcase is Found");
            Destroy(gameObject); // Destroy the briefcase when player collides with it (Collected)
        }
    }
}
