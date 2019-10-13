using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{

    /*
     * Below are just simple methods for loading the Designed Scene or Recreated Scene in the game.
     * This isonly included in the main menu.
     *
     */
    
    public void onDesigned()
    {
        SceneManager.LoadScene("WorkingLevel");
    }

    public void onRecreated()
    {
        SceneManager.LoadScene("RecreatedLevel");
    }

    public void QuitGame()
    {
        Debug.Log("Only Quit when outside of Unity Editor");
        Application.Quit();
    }

}
