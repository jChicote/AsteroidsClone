using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour
{
    public static int points;
    public Text scoreText;

    //This converts the points into string and outputs it too the score text.
    void Update()
    {
        scoreText.text = points.ToString();
    }
}
