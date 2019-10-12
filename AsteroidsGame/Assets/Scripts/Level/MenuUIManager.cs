using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{

    public void onDesigned()
    {
        SceneManager.LoadScene("WorkingLevel");
    }

    public void onRecreated()
    {
        SceneManager.LoadScene("RecreatedLevel");
    }

}
