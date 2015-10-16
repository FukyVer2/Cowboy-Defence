using UnityEngine;
using System.Collections;

public abstract class BaseBulletObject : BaseMoveObject {

    protected float damge; // Damge cua dan
    protected BaseBulletType bulletType; //Thong so chi loai dan
    protected BaseObjectType objectUseType; //Thong so chi loai dan nay do thang nao su dung

    public float Damge
    {
        get { return damge; }
        set { damge = value; }
    }

    public BaseBulletType BulletType
    {
        get { return bulletType; }
        set { bulletType = value; }
    }

    public BaseObjectType ObjectUseType
    {
        get { return objectUseType; }
        set { objectUseType = value; }
    }

    public override void InitObject()
    {
        base.InitObject();
        gameObjectType = BaseObjectType.OB_GUN;
    }


    public abstract void KillEnemies();
}
