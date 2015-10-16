using UnityEngine;
using System.Collections;

public abstract class BaseEnemyObject : BaseMoveObject
{

    public float damge; // Thong so chi luong damge cua moi con Enemy
    public float healthPoint; //Thong so chi luong mau cua enemy


    public override void InitObject()
    {
        base.InitObject();
        gameObjectType = BaseObjectType.OB_ENEMY;
    }

    public abstract void KillPlayer();
}
