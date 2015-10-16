using UnityEngine;
using System.Collections;

public class BaseGunObject : BaseObject {

    public BaseGunType gunType; //Thong so chi ve loai sung
    public BaseBulletType bulletType; //Thong so chi loai dan duoc su dung
    public BaseObjectType objectUseType; //Thong so chi loai sung nay do thang nao su dung
    public float dameOfGun; //Thong so chi luong damge cua sung, do la damge cua dan
    public float timeSpawnBullet; //Thoi gian san sinh ra mot vien dan
    public float timeReloadBullet; //Thoi gian nap lai dan
    public int cartridgeBoxSize; //Dung tich cau hop dan
    public GameObject bulletOfGun; //Thong so chi dan ma sung nay su dung

    protected bool allowShoot; //Duoc phep ban hay chua
    protected bool reloading; //Dang thay dan
    protected int quantumOfBullet; // So luong dan con lai trong hop

    public int quantumOfCartridgeBox;
    public bool isActive; //Sung co con ban duoc khong


    public override void InitObject()
    {
        isActive = true;
        reloading = false;
        ReloadBullet();
    }

    //Ham thay dan
    public void ReloadBullet()
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
        if (quantumOfBullet > 0)
        {
            //Tao mot vien dan tai day
            BaseBulletObject baseBullet = null;
            baseBullet.InitObject();
            baseBullet.BulletType = bulletType;
            baseBullet.ObjectUseType = objectUseType;
            baseBullet.Damge = dameOfGun;
            baseBullet.ResetValueOfAvariable();
            quantumOfBullet -= 1;
            Invoke("WaitShoot", timeSpawnBullet);
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
    }

    public override void DestroyObject()
    {
        //Khi sung khong hoat dong nua thi deactive cay sung do di
    }
}
