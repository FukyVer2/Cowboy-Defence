using UnityEngine;
using System.Collections;

public class Coin : BaseMoveItemObject {

    public const int COIN_POINT = 50; //Chi so coin can de sinh ra mot coin

    public override void InitObject()
    {
        base.InitObject();
        Move();
    }

    public override void ResetValueOfAvariable()
    {
        base.ResetValueOfAvariable();
        Move();
    }

    public override void Move()
    {
        Invoke("MoveTo", timeExists);
    }

    public void MoveTo()
    {
        iTween.MoveTo(gameObject, iTween.Hash(iT.MoveTo.time, 2.5f,
                                              iT.MoveTo.islocal, true,
                                              iT.MoveTo.position, positionMoveTo,
                                              iT.MoveTo.easetype, iTween.EaseType.linear,
                                              iT.MoveTo.oncomplete, "OnComplete"));
    }

    public void MoveTo(Vector3 _positionMoveTo)
    {
        iTween.MoveTo(gameObject, iTween.Hash(iT.MoveTo.time, 1.5f,
                                              iT.MoveTo.islocal, true,
                                              iT.MoveTo.position, _positionMoveTo,
                                              iT.MoveTo.easetype, iTween.EaseType.easeInBack,
                                              iT.MoveTo.oncomplete, "OnComplete"));
    }

    private void OnComplete()
    {
        Invoke("DestroyObject", 0.2f);
    }
}
