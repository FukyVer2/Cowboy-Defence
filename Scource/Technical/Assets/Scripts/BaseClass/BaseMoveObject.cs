using UnityEngine;
using System.Collections;

public abstract class BaseMoveObject : BaseObject, IMovement {

    public float velocityNormalX; //Van toc X ban dau
    public float velocityNormalY; //Van toc Y ban dau
    public float accelerationNormalX; // Gia toc X
    public float accelerationNormalY; // Gia toc Y

    protected float vx; // Van toc X tuc thoi
    protected float vy; // Van toc Y tuc thoi

    public BaseDirectionType direction; // Huong di chuyen

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
        //this.Move();
    }

    public abstract void Move();
}
