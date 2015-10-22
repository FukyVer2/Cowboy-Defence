using UnityEngine;
using System.Collections;

public abstract class BaseMoveObject : BaseObject, IMovement {

    public float velocityNormalX; //Van toc X ban dau
    public float velocityNormalY; //Van toc Y ban dau
    public float accelerationNormalX; // Gia toc X
    public float accelerationNormalY; // Gia toc Y

    public float vx; // Van toc X tuc thoi
    public float vy; // Van toc Y tuc thoi

    public BaseDirectionType direction; // Huong di chuyen

    // cài đặt ban đầu
    public override void InitObject()
    {
        vx = velocityNormalX;
        vy = velocityNormalY;
    }

    // reset object về trạng thái ban đầu
    public virtual void ResetValueOfAvariable()
    {
        vx = velocityNormalX;
        vy = velocityNormalY;
    }

    // update this
    public override void UpdateObject()
    {
        
    }

    public abstract void Move();
}
