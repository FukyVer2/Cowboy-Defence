using UnityEngine;
using System.Collections;

public abstract class BaseMoveItemObject : BaseMoveObject
{

    public float timeExists; // Thoi gian ton tai tren ban do
    protected Vector3 positionMoveTo; //Vi tri se duoc move toi

    public Vector3 PositionMoveTo
    {
        set { positionMoveTo = value; }
    }

    public override void InitObject()
    {
        base.InitObject();
        gameObjectType = BaseObjectType.OB_ITEM;
        positionBegin = transform.position;
    }

    public override void ResetValueOfAvariable()
    {
        base.ResetValueOfAvariable();
        positionBegin = transform.position;
    }

    public override void UpdateObject()
    {
        Move();
    }

    public override void Move()
    {
        
    }

    public override void DestroyObject()
    {
        PoolCustomize.Instance.HideBaseObject(gameObject, "Item");
    }
}
