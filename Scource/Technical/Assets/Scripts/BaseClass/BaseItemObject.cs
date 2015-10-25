using UnityEngine;
using System.Collections;

public class BaseItemObject : BaseMoveObject {

    public float timeExists; // Thoi gian ton tai tren ban do

    public override void InitObject()
    {
        base.InitObject();
        gameObjectType = BaseObjectType.OB_ITEM;
    }

    public override void Move()
    {
        
    }

    public override void DestroyObject()
    {
        
    }
}
