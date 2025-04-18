using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{

    // CHARACTER CONTROLLER
    private CharacterController controller;


    // MOVEMENT
    // Movement input for controller
    private Vector3 playerMovementInput;
    // Velocity for gravity and jump
    private Vector3 velocity;
    // Inputs and bools for walk, jog, jump 
    // May add a duck option later
    public float walkSpeed = 3f;
    public bool isWalking;

    public float jogSpeed = 6f;
    public bool isJogging;

    public float jumpForce = 5f;

    float currentSpeed;


    // GROUNDING
    // Negative value to pull the player down and ground them
    private float gravity = -9.81f;
    public bool isGrounded;


   



    // Start is called before the first frame update
    void Start()
    {
        // Time is true to real world time
        Time.timeScale = 1f;

        // Grab Character Controller
        controller = GetComponent<CharacterController>();

        // Lock cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Will need to make visible for options/inventory type screens, etc..

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

       

        // Movement Axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical"); // zInput gets players w or s input which is -1 or 1

        // Jogging Movement
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            isJogging = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isJogging = false;
        }

        // Velocity Movement
        velocity = transform.forward * zInput * currentSpeed + transform.right * horizontalInput * currentSpeed + Vector3.up * velocity.y;

    }

    private void MovePlayer()
    {
        isGrounded = controller.isGrounded;
        // If the player is grounded, the gravity resets
        if (isGrounded)
        {
            // Small Downward force to keep the player grounded
            velocity.y = -1f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = jumpForce;
            }
        }
        else
        {
            // Gravity is applied when player is in the air
            velocity.y -= gravity * -2f * Time.deltaTime;
        }

        // Jogging
        if (isJogging)
        {
            currentSpeed = jogSpeed;
            // Debug.Log("Current Speed Jog:" + currentSpeed);
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        // Move player on the x and z
        controller.Move(playerMovementInput * currentSpeed * Time.deltaTime);
        // Debug.Log("Current Speed:" + currentSpeed);
        // Apply vertical movement (gravity and jumping)
        controller.Move(velocity * Time.deltaTime);

    }

  



}
