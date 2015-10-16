using UnityEngine;
using System.Collections;

public abstract class BaseMoveObject : BaseObject, IMovement {

    public float velocityNormalX;
    public float velocityNormalY;
    public float accelerationNormalX;
    public float accelerationNormalY;

    protected float vx;
    protected float vy;

    public override void InitObject()
    {
        vx = velocityNormalX;
        vy = velocityNormalY;
        
    }

    public virtual void ResetValueOfAvariable()
    {
        vx = velocityNormalX;
        vy = velocityNormalY;
    }

    public override void UpdateObject()
    {
        this.Move();
    }

    public abstract void Move();
}
