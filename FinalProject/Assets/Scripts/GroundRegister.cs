using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

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
    private PlayerInput inputManagerScript;
    

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
        // This will always move the clipPlane toward distressClip every frame regardless of whether the player is actually far away from the path or not because 
        // We need to update FarClipPlane in update!!
        
        // We need to update the actual cameras farclipplane 
        float targetClip;
        if (onFarFromPath)
        {
            // If true target is 85f lerp gradually shrinks clip plane
            targetClip = distressClip;
        }
        else
        {
            // If false target is 220f lerp gradually expands clip plane again
            targetClip = normalClip;
        }

        // Smooth transition based off of distance
        clipPlane = Mathf.Lerp(clipPlane, targetClip, Time.deltaTime * 2f); //2f is the smoothing speed tweak as needed

        // Apply to virtual camera every frame
        virtualCamera.m_Lens.FarClipPlane = clipPlane;

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
