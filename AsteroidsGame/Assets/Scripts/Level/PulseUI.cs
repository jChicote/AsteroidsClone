using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseUI : MonoBehaviour
{
    public int numofPulses = 6;
    public Image[] pulseImages;
    public Sprite pulse;
    public Image pulseGroup;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("is running");

        if (WeaponController.weaponMode == 4)
        {
            pulseGroup.gameObject.SetActive(true);
            var pulses = GameObject.FindGameObjectsWithTag("bullet");
            var count = 0;

            //Counts the number of PulseBullets on the screen
            foreach (var i in pulses)
            {
                if (i.name == "PulseScatterBullet" || i.name == "PulseScatterBullet(Clone)")
                {
                    count++;
                }
            }

            //Finds the different the number of pulseBullets on the screen
            numofPulses = 6 - count;

            //Modifies the images within the array. Enabling and disabling their appearance.
            for (int i = 0; i < pulseImages.Length; i++)
            {
                if (i < numofPulses)
                {
                    pulseImages[i].enabled = true;
                }
                else
                {
                    pulseImages[i].enabled = false;
                }
            }
        } else
        {
            pulseGroup.gameObject.SetActive(false);
        }
    }
}
