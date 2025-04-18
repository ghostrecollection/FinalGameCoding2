using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 move;
    void OnMove(InputValue value)
    {
        // Move Inputs on x and z
        move = value.Get<Vector2>();
    }
}
