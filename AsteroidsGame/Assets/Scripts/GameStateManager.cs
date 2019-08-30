using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseUI;
    
    void Update()
    {

        //changes state to pause when escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Ecape key has been pressed");
            if (isPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }
}
