using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    // INPUTS
    // Vector2 for storing inputs
    public Vector2 move;
    public Vector2 look;
    public bool jog;
    public bool jump;


    // OnMove Function through Input System
    void OnMove(InputValue value)
    {
        // Move Inputs on x and z
        move = value.Get<Vector2>();
    }

    // OnLook Function through Input System
    void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }

    // OnJog Function through Input System
    void OnJog(InputValue value)
    {
        jog = value.isPressed;
    }

    // OnJump Function through Input System
    void OnJump(InputValue value)
    {
        jump = value.isPressed;
        Debug.Log("value is pressed: " + value);
    }

}
