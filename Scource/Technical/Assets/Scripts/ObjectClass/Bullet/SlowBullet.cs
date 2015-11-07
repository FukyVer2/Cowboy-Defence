using UnityEngine;
using System.Collections;

public class SlowBullet : BaseBulletObject
{

    public override void InitObject()
    {
        base.InitObject();
        bulletType = BaseBulletType.BL_SLOW;
        //direction = BaseDirectionType.UP;
        ResetValueOfAvariable();
    }

    public override void ResetValueOfAvariable()
    {
        BaseUtilExtentions.Instance.Rotate(transform, angleShoot - 90);

        vx = velocityNormalX * Mathf.Cos(angleShoot * Mathf.Deg2Rad);
        vy = velocityNormalX * Mathf.Sin(angleShoot * Mathf.Deg2Rad);

        if (vx == 0.0f)
            accelerationNormalX = 0;
        else if (vy == 0.0f)
            accelerationNormalY = 0;
    }

    //public override void UpdateObject()
    //{
    //    base.UpdateObject();
    //    KillEnemies();
    //}

    public override void KillEnemies()
    {

    }

    //Dan normal se di chuyen thang, dan nay do sung support hoac Enemy ban ra
    public override void Move()
    {
        vx += accelerationNormalX * Time.deltaTime;
        vy += accelerationNormalY * Time.deltaTime;

        //
        //vx = vx * Mathf.Cos(angelShoot);
        //vy = vx * Mathf.Sin(angelShoot);

        positionBegin.x += vx * Time.deltaTime;
        positionBegin.y += vy * Time.deltaTime;

        transform.position = positionBegin;
    }

    public override void DestroyObject()
    {
        base.DestroyObject();
    }
}
