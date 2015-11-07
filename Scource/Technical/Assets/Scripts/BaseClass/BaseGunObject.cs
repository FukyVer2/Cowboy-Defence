using UnityEngine;
using System.Collections;

public class BaseGunObject : BaseObject {

    public BaseGunType gunType; //Thong so chi ve loai sung
    public BaseBulletType bulletType; //Thong so chi loai dan duoc su dung
    public BaseObjectType objectUseType; //Thong so chi loai sung nay do thang nao su dung
    public BaseDirectionType directionOfShoot; //Huong ban cua dan
    public float dameOfGun; //Thong so chi luong damge cua sung, do la damge cua dan
    public float timeSpawnBullet; //Thoi gian san sinh ra mot vien dan
    public float timeReloadBullet; //Thoi gian nap lai dan
    public int cartridgeBoxSize; //Dung tich cau hop dan
    public GameObject bulletOfGun; //Thong so chi dan ma sung nay su dung

    public bool allowShoot; //Duoc phep ban hay chua
    protected bool reloading; //Dang thay dan
    protected int quantumOfBullet; // So luong dan con lai trong hop

    public int quantumOfCartridgeBox;
    public bool isActive; //Sung co con ban duoc khong
    public Transform bulletSpawnPosition; //Vi tri sinh ra bullet

    //Goc ban
    public float angleShoot; //Day la goc ban cua cay sung
    // Co them damge, stun
    public float critDamage; //Chi so crit damage
    public float critDamagePercent; //ty le crit damage
    public float stunPercent; // ty le stun
    //public Animator gunAnimator; //Animator cua gun
    //public float animationSpeed; //Toc do chuyen frame trang thai ban
    //public float totalFrame; //Tong so Frame
    //public int frameSpawnShoot; //Frame spawn ra dan


    public override void InitObject()
    {
        isActive = true;
        reloading = false;
        allowShoot = true;
        ReloadBullet();
    }

    //Ham thay dan
    public virtual void ReloadBullet()
    {
        reloading = false;
        if (quantumOfCartridgeBox > 0)
        {
            quantumOfBullet = cartridgeBoxSize;
            quantumOfCartridgeBox -= 1;
            if (!isActive)
                isActive = true;
        }
        else
            isActive = false; //Het dan roi pakon, vut sung thoi
    }

    //Ham spawn dan
    public void SpawnOfBullet()
    {
        if (quantumOfBullet > 0 && allowShoot)
        {
            //Tao mot vien dan tai day
            GameObject bullet = PoolCustomize.Instance.GetBaseObject(bulletOfGun, bulletSpawnPosition.position, "Bullet");
            float randomStun = (float)(Random.Range(0, 100)) / 100;
            float randomCrit = (float)(Random.Range(0, 100)) / 100;
            if (bullet != null)
            {
                BaseBulletObject baseBullet = bullet.GetComponent<BaseBulletObject>();
                baseBullet.BulletType = bulletType;
                baseBullet.ObjectUseType = objectUseType;
                baseBullet.Damge = dameOfGun;
                baseBullet.AngelShoot = angleShoot;
                baseBullet.direction = directionOfShoot;
                baseBullet.IsCritDamge = false;
                baseBullet.CritDamge = 0.0f;
                baseBullet.IsStun = false;
                if (critDamagePercent >= randomCrit && critDamagePercent > 0)
                {
                    baseBullet.IsCritDamge = true;
                    baseBullet.CritDamge = critDamage;
                }
                if (stunPercent >= randomStun && stunPercent > 0)
                {
                    baseBullet.IsStun = true;
                }
                baseBullet.InitObject();
                quantumOfBullet -= 1;
                //baseBullet.ResetValueOfAvariable();
                allowShoot = false;
                Invoke("WaitShoot", timeSpawnBullet);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning("Khong co dan nay ba");
#endif
            }
        }
    }

    //Ham cho phep spawn dan
    private void WaitShoot()
    {
        if (!allowShoot)
        {
            allowShoot = true;
        }
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
                reloading = true;
            }
        }
        DestroyObject();
        //SpawnOfBullet();
    }

    public override void DestroyObject()
    {
        //Khi sung khong hoat dong nua thi deactive cay sung do di
    }

    public virtual void GunRotate(Vector3 _position)
    {
        float angle = BaseUtilExtentions.Instance.CalAngleWithTarget(transform, _position);
        angleShoot = angle;
        BaseUtilExtentions.Instance.Rotate(transform, angle - 90);
        //Debug.Log("Goc quay cua sung: " + angle);
    }
}
