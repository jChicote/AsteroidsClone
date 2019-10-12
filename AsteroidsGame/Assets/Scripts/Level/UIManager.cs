using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseUI;

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

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        PointsController.points = 0;
        LevelSpawner.playerIsLost = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        PointsController.points = 0;
        LevelSpawner.playerIsLost = false;
    }
}
