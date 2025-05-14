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


    // Movement Script
    //PlayerCharacterController1 playerCharacterScript;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        /*playerCharacterScript = GetComponent<PlayerCharacterController1>();
        float walkValue = playerCharacterScript.walkSpeed;
        float runValue = playerCharacterScript.runSpeed;*/

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

            anim.Play("Nervous");
            //StartCoroutine(PausePlayerMovement());
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


    /*IEnumerator PausePlayerMovement(float walkValue, float runValue)
    {
        walkValue = 0;
        runValue = 0;
        anim.Play("Nervous");
        yield return new WaitForSeconds(4.5f);
        walkValue = 5f;
        runValue = 8f;
    }*/
    
}
