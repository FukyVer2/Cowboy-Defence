using UnityEngine;
using System.Collections;

public class BaseWallObject : BaseObject {

    public float healthPoint;


    public override void InitObject()
    {
        gameObjectType = BaseObjectType.OB_WALL;
    }

    public override void UpdateObject()
    {
        
    }

    public virtual void ReceiveDamge(float damge)
    {
        healthPoint -= damge;
    }

    public override void DestroyObject()
    {
        Debug.Log("destroy wall");
        ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_WALL_FALL, transform.position);
        Destroy(gameObject);
    }
}
