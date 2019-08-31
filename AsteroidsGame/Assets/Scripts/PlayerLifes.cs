using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifes : MonoBehaviour
{
    public static int numofLifes = 6;
    public Image[] lifes;
    public Sprite lifeIMG;

    private int gainHealth = 2000;

    void Update()
    {
        for (int i = 0; i < lifes.Length; i++)
        {
            if (i < numofLifes)
            {
                lifes[i].enabled = true;
            }
            else
            {
                lifes[i].enabled = false;
            }
        }

        if (PointsController.points >= gainHealth) {
            numofLifes += 1;
            gainHealth += 2000;
        }
    }
}
