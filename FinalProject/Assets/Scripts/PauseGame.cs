using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    // Pause screen and bool to know when paused
    public GameObject pauseScreen;
    private bool gameIsPaused;


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
        // Pauses time and opens pause screen
        if (gameIsPaused)
        {
            gameIsPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            // Lock cursor to middle of screen
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            gameIsPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            // Turns mouse back on
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
