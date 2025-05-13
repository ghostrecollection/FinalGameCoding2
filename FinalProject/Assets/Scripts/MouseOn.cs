using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Turns mouse back on
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    
}
