﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIGun : MonoBehaviour {

    public BaseGunType gunType;
    public Image hideImage;
    public CanvasGroup canvasGroup;
    public Text quantumOfCartridgeBox;
    private float timeCurrent;
    public float timeReloadBullet;
    public bool reloading;


    public void ChangeGunHandelEvent()
    {
        GameController.Instance.player.ChangeGun(gunType);
    }

    public void SetQuantumOfCartridgeBoxText(int _cartridgeBox)
    {
        quantumOfCartridgeBox.text = _cartridgeBox.ToString();
    }

    public void SetTimeReloadBullet(float _timeReloadBullet)
    {
        timeReloadBullet = _timeReloadBullet;
    }

    public void ReloadBullet()
    {
        reloading = true;
        canvasGroup.blocksRaycasts = false;
        hideImage.fillAmount = 1;
    }


    void Update()
    {
        if (reloading)
        {
            timeCurrent += Time.deltaTime;
            if (timeCurrent >= timeReloadBullet)
            {
                canvasGroup.blocksRaycasts = true;
                timeCurrent = 0.0f;
                reloading = false;
            }
            else
            {
                hideImage.fillAmount = BaseUtilExtentions.Instance.RoundToFloat(1 - (timeCurrent / timeReloadBullet), 1);
            }
        }
    }
}
