using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacterController : MonoBehaviour
{
    // SPEED
    [SerializeField] float speed = 3f;

    // CHARACTER CONTROLLER
    private CharacterController controller;

    // MOVEMENT AND INPUTS
    // Script reference for move vector
    private PlayerInputManager input;




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
       
        // Target direction based of the x and z inputs
        Vector3 targetDirection = new Vector3(input.move.x, 0, input.move.y);
        controller.Move(targetDirection * speed * Time.deltaTime);
        // Target rotation -- player rotates to direction input
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        // Smooths rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime);

    }

    
  



}
