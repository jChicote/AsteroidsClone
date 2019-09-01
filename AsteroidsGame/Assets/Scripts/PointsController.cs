using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour
{
    public static int points;
    public Text scoreText;

    void Update()
    {
        scoreText.text = points.ToString();
    }
}
