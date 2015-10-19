using UnityEngine;
using System.Collections;

public class AutoGun : BaseGunObject, IAnimatedSprite {

    public Animator gunAnimator; //Animator cua gun
    //public int frameSpawnShoot; //Frame spawn ra dan
    //private float animationSpeedBegin; //Toc do speed ban dau
    //private float animationSpeed; //Toc do speed o trang thai ban


    public override void InitObject()
    {
        base.InitObject();
        //this.CalAnimationSpeed();
    }

    public override void UpdateObject()
    {
        base.UpdateObject();
        GunShoot();
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

    public void CalAnimationSpeed()
    {
        //animationSpeed = ((float)1 / 60) / (timeSpawnBullet / (float)frameSpawnShoot);
        //if (timeSpawnBullet == 0)
        //    animationSpeed = 0.0f;
        //else
            //animationSpeed = 0.3f * ((timeSpawnBullet / frameSpawnShoot) * 60);
        //animationSpeed = (animationSpeed < gunAnimator.speed) ? gunAnimator.speed : animationSpeed;
        //Debug.Log("Speed: " + animationSpeed);
    }
}
