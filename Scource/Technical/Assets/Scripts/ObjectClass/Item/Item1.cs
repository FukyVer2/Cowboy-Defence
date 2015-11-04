using UnityEngine;
using System.Collections;

public class Item1 : BaseItemObject {
    public override void InitObject()
    {
        base.InitObject();
    }
    public override void Move()
    {
        positionBegin = transform.position;
        switch(direction)
        {
            case BaseDirectionType.DOWN:
                positionBegin.y += vy * Time.deltaTime;
                break;
        }
        transform.position = positionBegin;
    }
    public override void UpdateObject()
    {
        Move();
    }
    void OnTriggerEnter2D(Collider2D col)
    {

    }
}
