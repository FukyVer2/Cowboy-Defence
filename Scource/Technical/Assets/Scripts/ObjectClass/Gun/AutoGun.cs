using UnityEngine;
using System.Collections;

public class AutoGun : BaseGunObject, IAnimatedSprite {

    public Animator gunAnimator; //Animator cua gun
    //public int frameSpawnShoot; //Frame spawn ra dan
    //private float animationSpeedBegin; //Toc do speed ban dau
    //private float animationSpeed; //Toc do speed o trang thai ban
    public RangeOfGun rangeOfGun;

    public override void InitObject()
    {
        base.InitObject();
        //this.CalAnimationSpeed();
    }

    public override void UpdateObject()
    {
        base.UpdateObject();
        if (rangeOfGun != null)
        {
            //if (rangeOfGun.enemyInBoxs.Count != 0)
            //{
            //    BaseEnemyObject baseEnemy = null;
            //    for (int i = 0; i < rangeOfGun.enemyInBoxs.Count; ++i )
            //    {
            //        baseEnemy = rangeOfGun.enemyInBoxs[i];
            //        if (baseEnemy.healthPoint <= 0)
            //        {
            //            rangeOfGun.enemyInBoxs.Remove(baseEnemy);
            //            baseEnemy = null;
            //        }
            //        else
            //            break;
            //    }

            //    if(baseEnemy != null)
            //        GunShoot(baseEnemy.transform.position);
            //    else if (rangeOfGun.enemyInBoxs.Count == 0)
            //    {
            //        transform.localRotation = Quaternion.identity;
            //        //Quaternion.Slerp(transform.localRotation, Quaternion.identity, 0.5f);
            //    }
            //}
            //else
            //{
            //    if (transform.localRotation != Quaternion.identity)
            //    {
            //        transform.localRotation  = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime);
            //    }
            //}
            BaseEnemyObject baseEnemy = GetEnemyInRange();
            if (baseEnemy != null)
                GunShoot(baseEnemy.transform.position);
            else if (rangeOfGun.enemyInBoxs.Count == 0)
            {
                if (transform.localRotation != Quaternion.identity)
                {
                    transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime);
                }
                //transform.localRotation = Quaternion.identity;
                //Quaternion.Slerp(transform.localRotation, Quaternion.identity, 0.5f);
            }
        }
        
    }

    public BaseEnemyObject GetEnemyInRange()
    {
        BaseEnemyObject baseEnemy = null;
        BaseEnemyObject baseEnemyTarget = null;
        float minSpace = 0.0f;
        if (rangeOfGun.enemyInBoxs.Count != 0)
        {
            minSpace = rangeOfGun.enemyInBoxs[0].transform.position.y;
            baseEnemyTarget = rangeOfGun.enemyInBoxs[0];
            for (int i = 0; i < rangeOfGun.enemyInBoxs.Count; ++i)
            {
                baseEnemy = rangeOfGun.enemyInBoxs[i];
                if (baseEnemy.healthPoint <= 0)
                {
                    rangeOfGun.enemyInBoxs.Remove(baseEnemy);
                }
                else
                {
                    if (baseEnemy.transform.position.y < minSpace)
                    {
                        minSpace = baseEnemy.transform.position.y;
                        baseEnemyTarget = baseEnemy;
                    }
                }
            }

            //if (baseEnemyTarget != null)
            //    GunShoot(baseEnemyTarget.transform.position);
            //else if (rangeOfGun.enemyInBoxs.Count == 0)
            //{
            //    transform.localRotation = Quaternion.identity;
            //    //Quaternion.Slerp(transform.localRotation, Quaternion.identity, 0.5f);
            //}
        }
        return baseEnemyTarget;

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

    public void GunShoot(Vector3 enemyPosition)
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
                AudioManager.Instance.PlayOneShot(BaseAudioType.BA_GUN_SHOOT_AUDIO);
                GunRotate(enemyPosition);
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
