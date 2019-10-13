using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseUI;
    public GameObject deathUI;

    void Update()
    {
        //changes state to pause when escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //public function for resume buttons
    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    //public function for pausing scene
    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    //public function for restarting current scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseUI.SetActive(false);
        deathUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        PointsController.points = 0;
        LevelSpawner.playerIsLost = false;
    }

    //public function for quitting to main menu
    public void Quit()
    {
        SceneManager.LoadScene("Menu");
        pauseUI.SetActive(false);
        deathUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        PointsController.points = 0;
        LevelSpawner.playerIsLost = false;
    }
}
