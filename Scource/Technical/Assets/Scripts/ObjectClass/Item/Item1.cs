using UnityEngine;
using System.Collections;

public class Item1 : BaseItemObject {
    private bool isMove = true;
    public bool isExplosion = false;

    public float timeWaitExplosion = 0.0f;

    public CircleCollider2D collider;
    public float damge = 0.0f;
    public GameObject particalExplosion;
    public override void InitObject()
    {
        isMove = true;
        collider.enabled = false;
        StartCoroutine(WaitExplosionBom(timeWaitExplosion));
        base.InitObject();
    }

    //bom sau bao nhiu giay
    IEnumerator WaitExplosionBom(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
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
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject.tag == "Enemy")
        //{
        //    BaseEnemyObject baseEnemy = col.gameObject.GetComponent<BaseEnemyObject>();
        //    if (baseEnemy.healthPoint > 0)
        //    {
        //        ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_BOM_ITEM_EXPLOSION, col.gameObject.transform.position);
        //        baseEnemy.ReceiveDamge(this.damge);
        //        PoolCustomize.Instance.HideBaseObject(gameObject, "Item", 0.3f);
        //    }
        //}
    }

    IEnumerator waitingBom(float time,Vector3 pos)
    {
        yield return new WaitForSeconds(time);
        Instantiate(particalExplosion, pos, Quaternion.identity);
    }
}
