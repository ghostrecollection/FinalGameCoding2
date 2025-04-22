using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacterController : MonoBehaviour
{
    // NOTES TO ADD: Camera smoothing, jump, grounding

    // CHARACTER CONTROLLER
    private CharacterController controller;

    // MOVEMENT AND INPUTS
    // Script reference for move vector
    private PlayerInputManager input;

    // Movements
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float jogSpeed = 5f;
    [SerializeField] float jumpForce = 3f;

    // GROUNDING
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;
    public bool isGrounded;
    // Layer for ground detection
    public LayerMask groundLayer;
    // Empty location at players feet
    public Transform groundCheck;
    // Raycast distance for ground detection
    private float groundDistance;


    // CAMERA
    // Camera Reference
    [SerializeField] GameObject mainCam;
    // Transform that is tracked for camera movement
    [SerializeField] Transform cameraFollowTarget;
    // Rotation and Sensitivity Inputs
    float xRotation;
    float yRotation;
    public float mouseSensitivity = 20f;




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

    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0;
        // Target direction based of the x and z inputs
        Vector3 inputDir = new Vector3(input.move.x, 0, input.move.y);
        // Target rotation -- player rotates to direction input
        float targetRotation = 0;

        

        JumpAndGravity();

        if(input.move != Vector2.zero)
        {
            // Jogging
            if (input.jog)
            {
                speed = jogSpeed;
            }
            else
            {
                speed = walkSpeed;
            }

            targetRotation = Quaternion.LookRotation(inputDir).eulerAngles.y + mainCam.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
            // Smooths movement rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);

            

        }
        Vector3 targetDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        controller.Move(targetDirection * speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance);
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
        xRotation = Mathf.Clamp (xRotation, -20, 60);
        // Uses mouse input from y to rotate on x
        yRotation += input.look.x * mouseSensitivity * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler (xRotation, yRotation, 0);
        cameraFollowTarget.rotation = rotation;
    }
  

    void JumpAndGravity()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            if (input.jump)
            {
                velocity.y = jumpForce;
                input.jump = false;
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

    
}
