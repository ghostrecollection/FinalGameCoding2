using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKeyToBegin : MonoBehaviour
{
    // NOTES
    // I want to adjust this so it is a bit less quick,


    // Bool to keep track of if the player pressed a key
    private bool keyPressed = false;
    
    private void Start()
    {
        // Lock cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Will need to make visible for options/inventory type screens, etc..
    }


    private void Update()
    {
        // Uses any key input for scene loading
        if (!keyPressed && Input.anyKeyDown)
        {
            keyPressed = true;
            SceneManager.LoadScene(1);
        }
    }


}
