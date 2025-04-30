using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class GroundRegister : MonoBehaviour
{
    // NOTES

    // Goal of this script is to register when the player is on the path versus when they
    // stray further and further away so different things can happen as a result

    // Locational bools to know when the player is where
    [SerializeField] bool onMainPath, onNearPath, onFarFromPath;
    // Script Reference
    [SerializeField] PlayerCharacterController playerCharacterControllerScript;

    [SerializeField] Ray ray;
    [SerializeField] RaycastHit hit;

    [SerializeField] float rayLength = .4f;



    // Start is called before the first frame update
    void Start()
    {
        playerCharacterControllerScript = GetComponent<PlayerCharacterController>();
        Physics.Raycast(playerCharacterControllerScript.groundCheck.position, Vector3.down, rayLength, playerCharacterControllerScript.groundLayer);

        GroundCheck();
    }


    // Tracks when player is at certain locations
    private void GroundCheck()
    {
        if (hit.transform.CompareTag("MainPath"))
        {
            // TRUE LOCATION
            onMainPath = true;
            onNearPath = false;
            onFarFromPath = false;
        }

        if (hit.transform.CompareTag("NearPath"))
        {
            onMainPath = false;
            // TRUE LOCATION
            onNearPath = true;
            onFarFromPath = false;
        }

        if (hit.transform.CompareTag("FarFromPath"))
        {
            onMainPath = false;
            onNearPath = false;
            // TRUE LOCATION
            onFarFromPath = true;
        }




    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
