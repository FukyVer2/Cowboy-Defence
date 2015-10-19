using UnityEngine;
using System.Collections;

public class ControlGun : BaseGunObject, IAnimatedSprite
{

    public Animator gunAnimator; //Animator cua gun
    //public int frameSpawnShoot; //Frame spawn ra dan
    //private float animationSpeedBegin; //Toc do speed ban dau
    //private float animationSpeed; //Toc do speed o trang thai ban


    public override void InitObject()
    {
        base.InitObject();
        //this.CalAnimationSpeed();
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
                this.SpawnOfBullet();
            }
        }
    }

    public void CalAnimationSpeed()
    {
        throw new System.NotImplementedException();
    }
}
