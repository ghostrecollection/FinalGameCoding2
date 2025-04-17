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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
