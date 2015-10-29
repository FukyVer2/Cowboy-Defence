using UnityEngine;
using System.Collections;

public class Coin : BaseMoveItemObject {

    public const int COIN_POINT = 50; //Chi so coin can de sinh ra mot coin
    public float timeMove; //Thoi gian dong tien se bung ra
    private bool isMove; //Bien de chi trang thai dong tien move hay chua

    public override void InitObject()
    {
        //base.InitObject();
        Invoke("WaitMoveUp", timeMove);
        isMove = false;
    }

    public override void ResetValueOfAvariable()
    {
        base.ResetValueOfAvariable();
        //vx = velocityNormalX;
        vy = velocityNormalY;
        Invoke("WaitMoveUp", timeMove);
        isMove = false;
        
    }

    public override void Move()
    {
        if (!isMove)
        {
            vx += accelerationNormalX * Time.deltaTime;
            vy -= accelerationNormalY * Time.deltaTime;

            //
            //vx = vx * Mathf.Cos(angelShoot);
            //vy = vx * Mathf.Sin(angelShoot);

            positionBegin.x += vx * Time.deltaTime;
            positionBegin.y += vy * Time.deltaTime;

            transform.position = positionBegin;
        }
        //Invoke("MoveTo", timeExists);
    }

    public void MoveTo()
    {
        iTween.MoveTo(gameObject, iTween.Hash(iT.MoveTo.time, 1.0f,
                                              iT.MoveTo.position, positionMoveTo,
                                              iT.MoveTo.easetype, iTween.EaseType.linear,
                                              iT.MoveTo.oncomplete, "OnComplete"));
    }

    public void MoveTo(Vector3 _positionMoveTo)
    {
        iTween.MoveTo(gameObject, iTween.Hash(iT.MoveTo.time, 1.0f,
                                              iT.MoveTo.position, _positionMoveTo,
                                              iT.MoveTo.easetype, iTween.EaseType.easeInBack,
                                              iT.MoveTo.oncomplete, "OnComplete"));
    }

    public void WaitMoveUp()
    {
        isMove = true;
        Invoke("MoveTo", timeExists);
    }

    private void OnComplete()
    {
        Invoke("DestroyObject", 0.2f);
    }
}
