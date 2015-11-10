using UnityEngine;
using System.Collections;

public class ControlGun : BaseGunObject, IAnimatedSprite
{

    public Animator gunAnimator; //Animator cua gun
    //public int frameSpawnShoot; //Frame spawn ra dan
    //private float animationSpeedBegin; //Toc do speed ban dau
    //private float animationSpeed; //Toc do speed o trang thai ban
    public bool auto;
    //UI
    private UIGun uiGun;

    public override void InitObject()
    {
        isActive = true;
        reloading = false;
        allowShoot = true;
        //UI
        uiGun = UIGunController.Instance.GetGunUI(gunType);
        uiGun.SetTimeReloadBullet(timeReloadBullet);
        uiGun.SetQuantumOfCartridgeBoxText(quantumOfCartridgeBox);
        //
        Invoke("ReloadBullet", timeReloadBullet);
        uiGun.ReloadBullet();
        //this.CalAnimationSpeed();
    }

    public override void ReloadBullet()
    {
        reloading = false;
        if (quantumOfCartridgeBox > 0)
        {
            //UI
            uiGun.SetQuantumOfCartridgeBoxText(quantumOfCartridgeBox);
            //
            quantumOfBullet = cartridgeBoxSize;
            quantumOfCartridgeBox -= 1;
            if (!isActive)
                isActive = true;
        }
        else
            isActive = false; //Het dan roi pakon, vut sung thoi
    }

    public override void UpdateObject()
    {
        if (isActive && quantumOfCartridgeBox <= 0)
            return;
        if (quantumOfBullet <= 0)
        {
            if (!reloading)
            {
                //Thay dan
                Invoke("ReloadBullet", timeReloadBullet);
                //UI
                uiGun.ReloadBullet();
                //
                reloading = true;
            }
        }
        DestroyObject();
        //SpawnOfBullet();
    }

    public void GunStop()
    {
        gunAnimator.SetBool("isShoot", false);
        //gunAnimator.speed = animationSpeedBegin;
    }

    public void GunShoot()
    {
        if (isActive)
        {
            if (allowShoot && !reloading)
            {
                //this.CalAnimationSpeed();
                //animationSpeedBegin = gunAnimator.speed;
                //gunAnimator.speed = animationSpeed;
                gunAnimator.SetBool("isShoot", true);
                //
                this.SpawnOfBullet();
            }
        }
    }

    public void GunShoot(Vector3 _mousePosition)
    {
        if (isActive)
        {
            if (allowShoot && !reloading)
            {
                //this.CalAnimationSpeed();
                //animationSpeedBegin = gunAnimator.speed;
                //gunAnimator.speed = animationSpeed;
                GunRotate(_mousePosition);
                gunAnimator.SetBool("isShoot", true);
                //
                AudioManager.Instance.PlayOneShot(BaseAudioType.BA_AK_GUN_SHOOT);
                this.SpawnOfBullet();
            }
        }
    }

    public void CalAnimationSpeed()
    {
        throw new System.NotImplementedException();
    }
}
