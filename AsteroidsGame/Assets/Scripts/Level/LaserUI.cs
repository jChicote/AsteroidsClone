using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserUI : MonoBehaviour
{
    public static float batteryCharge = 100;
    public RectTransform foregroundBar;
    public Image weaponCharge;

    private float initialWidth;
    private float motifyWidth;

    void Start()
    {
        initialWidth = foregroundBar.rect.width;
        motifyWidth = initialWidth;
    }

    // Update is called once per frame
    void Update()
    {
        //Modifies the foreground bar representing the level of charge on the laser.
        foregroundBar.sizeDelta = new Vector2(motifyWidth * (batteryCharge / 100f), foregroundBar.rect.height);

        //If the laserbeam isn't held it is recharged
        if (laserBeam.isHeld == false) Recharge();

        if (WeaponController.weaponMode == 3)
        {
            weaponCharge.gameObject.SetActive(true);
        }
        else
        {
            batteryCharge = 100;
            weaponCharge.gameObject.SetActive(false);
            motifyWidth = initialWidth;
            foregroundBar.sizeDelta = new Vector2(initialWidth, foregroundBar.rect.height);
        }
    }

    //Recharges the laser charge
    void Recharge()
    {
        if (batteryCharge <= 100) batteryCharge += 0.5f;
    }
}
