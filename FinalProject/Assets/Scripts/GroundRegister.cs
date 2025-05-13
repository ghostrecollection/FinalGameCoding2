using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRegister : MonoBehaviour
{
    // NOTES
    // Goal of this script is to register when the player is on the path versus when they
    // stray further and further away so different things can happen as a result

    // 

    // Locational bools to know when the player is where
    [SerializeField] bool onMainPath, onNearPath, onFarFromPath;
    

    // Start is called before the first frame update
    void Start()
    {
        
       
    }


    // Tracks when player is at certain locations
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainPath"))
        {
            //Debug.Log("MainPath Collided:");
            // TRUE LOCATION
            onMainPath = true;
            onNearPath = false;
            onFarFromPath = false;
        }

        if (other.gameObject.CompareTag("NearPath"))
        {
            onMainPath = false;
            // TRUE LOCATION
            onNearPath = true;
            onFarFromPath = false;
        }

        if (other.gameObject.CompareTag("FarFromPath"))
        {
            onMainPath = false;
            onNearPath = false;
            // TRUE LOCATION
            onFarFromPath = true;
        }


    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MainPath"))
        {
            // Exiting the area
            onMainPath = false;
            
        }

        if (other.gameObject.CompareTag("NearPath"))
        {
            // Exiting the area
            onNearPath = false;
            
        }

        if (other.gameObject.CompareTag("FarFromPath"))
        {
            // Exiting the area
            onFarFromPath = true;
        }

    }


    
}
