using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifes : MonoBehaviour
{
    public static int numofLifes = 6;
    public Image[] lifes;
    public Sprite lifeIMG;

    public AudioSource lifeAudio;
    public AudioClip life_gained;

    private int gainHealth = 2000;

    void Start()
    {
        lifeAudio = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
        numofLifes = 6;
    }

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
            if(numofLifes < 6)
            {
                numofLifes += 1;
                lifeAudio.PlayOneShot(life_gained);
            }
            gainHealth += 2000;
        }
    }
}
