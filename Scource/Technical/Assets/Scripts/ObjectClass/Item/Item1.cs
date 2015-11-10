using UnityEngine;
using System.Collections;

public class Item1 : BaseItemObject {
    private bool isMove ;
    public bool isExplosion;

    private float timeDelay = 0.0f;
    public float timeWaitExplosion = 0.0f;

    public CircleCollider2D collider;
    public float damge = 0.0f;

    public GameObject objUmbrella;
    public override void InitObject()
    {
        isMove = true;
        isExplosion = true;
        collider.enabled = false;
        //StartCoroutine(WaitExplosionBom(timeWaitExplosion));
        base.InitObject();
    }

    //bom sau bao nhiu giay
    IEnumerator WaitExplosionBom(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isMove = false;
        collider.enabled = true;
    }

    void ExplosionBom()
    {
        isMove = false;
        collider.enabled = true;
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
        if(isMove)
        {
            Move();
        }
        if(isMove && (timeDelay+= Time.deltaTime) >= timeWaitExplosion)
        {
            timeDelay = 0.0f;
            objUmbrella.SetActive(false);
            ExplosionBom();
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
         
    }
}
