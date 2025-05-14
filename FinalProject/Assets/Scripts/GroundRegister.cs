using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GroundRegister : MonoBehaviour
{
    // NOTES
    // Goal of this script is to register when the player is on the path versus when they
    // stray further and further away so different things can happen as a result

    // 

    // Locational bools to know when the player is where
    [SerializeField] bool onMainPath, onFarFromPath;
    // Camera reference
    public CinemachineVirtualCamera virtualCamera;
    // Animator
    Animator anim;

    public float clipPlane;
    public float normalClip = 220f;
    public float distressClip = 85f;


    // Movement and Input Script
    PlayerCharacterController1 playerCharacterScript;
    PlayerInput inputManagerScript;
    

    // Start is called before the first frame update
    void Start()
    {
        // Animator component reference
        anim = GetComponentInChildren<Animator>();
        // Script Reference
        playerCharacterScript = GetComponent<PlayerCharacterController1>();
        inputManagerScript = GetComponent<PlayerInput>();
        // Clip plane
        clipPlane = normalClip;
       
    }

    void Update()
    {
        // This is the section I need help with
        clipPlane = Mathf.MoveTowards(clipPlane, distressClip, 85f * Time.deltaTime);
    }
    // Tracks when player is at certain locations
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainPath"))
        {
            //Debug.Log("MainPath Collided:");
            // TRUE LOCATION
            onMainPath = true;
            onFarFromPath = false;

            virtualCamera.m_Lens.FarClipPlane = 220f;
        }

       
        if (other.gameObject.CompareTag("FarFromPath"))
        {
            onMainPath = false;
            // TRUE LOCATION
            onFarFromPath = true;

            
            virtualCamera.m_Lens.FarClipPlane = clipPlane;

            
            StartCoroutine(PausePlayerMovement());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MainPath"))
        {
            // Exiting the area
            onMainPath = false;
            
        }


        if (other.gameObject.CompareTag("FarFromPath"))
        {
            // Exiting the area
            onFarFromPath = false;
            virtualCamera.m_Lens.FarClipPlane = 220f;
        }

    }


    IEnumerator PausePlayerMovement()
    {
        // Checking if these scripts are active and temp turns them off
        if(playerCharacterScript != null)
        {
            playerCharacterScript.enabled = false; 
        }
        if(inputManagerScript != null)
        {
            inputManagerScript.enabled = false; 
        }
        // Plays animation and waits for a bit of time
        anim.Play("Nervous");
        yield return new WaitForSeconds(2.5f);
        // Turns the scripts back on 
        playerCharacterScript.enabled = true;
        inputManagerScript.enabled = true;
    }
    
}
