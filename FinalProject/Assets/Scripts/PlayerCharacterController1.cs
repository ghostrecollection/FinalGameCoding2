using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController1 : MonoBehaviour
{
    // NOTES TO ADD: Camera smoothing 

    // CHARACTER CONTROLLER
    private CharacterController controller;

    // MOVEMENT AND INPUTS
    // Script reference for move vector
    private PlayerInputManager input;

    // Movements
    public static float walkSpeed = 5f;
    public static float jogSpeed = 8f;
    float jumpForce = 7f;
    bool jumpActive;
    public float currentSpeed = 0;


    // GROUNDING
    float gravity = -9.81f;
    Vector3 velocity;
    public bool isGrounded;
    // Layer for ground detection
    public LayerMask groundLayer;
    // Empty location at players feet
    public Transform groundCheck;
    // Raycast distance for ground detection
    private float groundDistance = .4f;


    // CAMERA
    // Camera Reference
    public GameObject mainCam;
    // Transform that is tracked for camera movement
    public Transform cameraFollowTarget;
    // Rotation and Sensitivity Inputs
    float xRotation;
    float yRotation;
    public float mouseSensitivity = 20f;


    // SOUND
    // Specific audio clips
    public AudioSource walkingSFX, landingSFX;

    // ANIMATION
    private Animator anim;
    // Target floats to switch between animations in blend tree
    float idleTarget = 0;
    float walkTarget = 1;
    float jogTarget = 2;
    // Current Value the animator is at
    float idleCurrentValue = 0;
    float walkCurrentValue = 1;
    // static float startValue = 0;
    // How long to switch between animations
    float duration = 2f;




    // Start is called before the first frame update
    void Start()
    {
        // Time is true to real world time
        Time.timeScale = 1f;

        // Grab Player Input Manager Script
        input = GetComponent<PlayerInputManager>();
        // Grab Character Controller
        controller = GetComponent<CharacterController>();

        // Lock cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Will need to make visible for options/inventory type screens, etc..

        // Assigning sound effects
        walkingSFX = GameObject.FindGameObjectWithTag("MM").GetComponents<AudioSource>()[0];
        landingSFX = GameObject.FindGameObjectWithTag("MM").GetComponents<AudioSource>()[1];

        // Finding Animator
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        OnMove();
        // Jump and Grounding
        JumpAndGravity();
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector3.down, Color.yellow);



    }


    private void LateUpdate()
    {
        CameraRotation();
    }

    void CameraRotation()
    {
        // Uses mouse input from x to rotate on y
        // -= to invert mouse look
        xRotation -= input.look.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -10, 40);
        // Uses mouse input from y to rotate on x
        yRotation += input.look.x * mouseSensitivity * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        cameraFollowTarget.rotation = rotation;
    }


    void JumpAndGravity()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            if (input.jump)
            {
                anim.SetBool("jumpActive", jumpActive);
                velocity.y = jumpForce;
                input.jump = false;

                landingSFX.Play();
            }
        }
        else
        {
            // Gravity is applied when player is in the air
            velocity.y -= gravity * -2f * Time.deltaTime;
        }

        // Apply vertical movement (gravity and jumping)
        controller.Move(velocity * Time.deltaTime);

    }

    public void OnMove()
    {
        currentSpeed = 0;
        // Debug.Log($"currentSpeed: {currentSpeed}");

        // Target direction based of the x and z inputs
        Vector3 inputDir = new Vector3(input.move.x, 0, input.move.y);
        // Target rotation -- player rotates to direction input
        float targetRotation = 0;

        if (input.move != Vector2.zero)
        {

            if (!walkingSFX.isPlaying)
            {
                walkingSFX.Play();
            }

            // Jogging
            if (input.jog)
            {
                currentSpeed = jogSpeed;
                // Animated Jog based off blend tree values
                anim.SetFloat("Speed", jogTarget);
            }
            else
            {
                currentSpeed = walkSpeed;
                // Animated Walk based off blend tree values
                if (currentSpeed < 0.5)
                {
                    // If the player slows to stop moving, smooth to idle animation
                    //StartCoroutine(WalkToIdleTransition());
                    anim.SetFloat("Speed", walkTarget);
                }
                else
                {
                    // If they are moving, keep playing walk animation
                    anim.SetFloat("Speed", walkCurrentValue);
                }
                Debug.Log($"walk target: {walkTarget}");
            }

            targetRotation = Quaternion.LookRotation(inputDir).eulerAngles.y + mainCam.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
            // Smooths movement rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);


        }
        else
        {
            walkingSFX.Stop();
            // Animated Movement based off blend tree values
            if (currentSpeed > 0)
            {
                // If the player is moving, smooth to walk animation
                // StartCoroutine(IdleToWalkTransition());
                anim.SetFloat("Speed", idleTarget);
            }
            else
            {
                // If they are not, just play idle animation
                anim.SetFloat("Speed", idleCurrentValue);
            }

            Debug.Log($"idle target: {idleTarget}");
        }

        Vector3 targetDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        controller.Move(targetDirection * currentSpeed * Time.deltaTime);


    }

    // This is the section I need help with it applies to the 
    IEnumerator IdleToWalkTransition()
    {
        yield return new WaitForSeconds(1f);
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / 2f;
            // IDLE TO WALK - smooth transition that takes idle value and walk value and moves towards using the duration
            anim.SetFloat("Speed", t);
            yield return null;
        }
        
    }



    /*IEnumerator WalkToIdleTransition()
    {
        for (float f = 0; f <= duration; f += Time.deltaTime)
        {
            // IDLE TO WALK - smooth transition that takes idle value and walk value and moves towards using the duration
            idleTarget = Mathf.Lerp(walkTarget, idleTarget, f / duration);


            yield return null;
        }


        for (float f = 0; f <= duration; f += Time.deltaTime)
        {
            // IDLE TO WALK - smooth transition that takes idle value and walk value and moves towards using the duration
            idleTarget = Mathf.Lerp(idleTarget, walkTarget, f / duration);
            f += 0.5f * Time.deltaTime;

            if (f > 1.0f)
            {
                float temp = walkTarget;
                walkTarget = idleTarget;
                idleTarget = temp;
                f = 0.0f;
            }


            yield return null;
        }
    }*/
}
