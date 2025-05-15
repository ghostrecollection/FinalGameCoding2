using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    // Pause screen and bool to know when paused
    public GameObject pauseScreen;
    private bool gameIsPaused;

    // Movement and Input Script
    PlayerCharacterController1 playerCharacterScript;
    private PlayerInput inputManagerScript;



    void Start()
    {
        // Script Reference
        playerCharacterScript = GetComponent<PlayerCharacterController1>();
        inputManagerScript = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnPause();
        }

        if(Time.timeScale > 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void OnPause()
    {
        
        if (gameIsPaused)
        {
            // Makes game back to normal
            gameIsPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;

            // Lock cursor to middle of screen
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Turns the scripts back on 
            playerCharacterScript.enabled = true;
            inputManagerScript.enabled = true;
            
        }
        else
        {
            // Pauses time and opens pause screen

            gameIsPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;

            // Turns mouse back on
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Checking if these scripts are active and temp turns them off
            if (playerCharacterScript != null)
            {
                playerCharacterScript.enabled = false;
            }
            if (inputManagerScript != null)
            {
                inputManagerScript.enabled = false;
            }
        }

    }

    public void OnResume()
    {
        // Lock cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Lets player pick up where they left off
        gameIsPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        // Turns the scripts back on 
        playerCharacterScript.enabled = true;
        inputManagerScript.enabled = true;

    }

    public void OnMainMenu()
    {
        // Lock cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Takes player to title screen
        SceneManager.LoadScene(0);
    }

    public void OnRestart()
    {
        // Lock cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Reloads the scene
        SceneManager.LoadScene(1);
    }


}
