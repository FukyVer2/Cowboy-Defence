using UnityEngine;
using System.Collections;

public abstract class BaseBulletObject : BaseMoveObject {

    protected float damge; // Damge cua dan
    protected BaseBulletType bulletType; //Thong so chi loai dan
    protected BaseObjectType objectUseType; //Thong so chi loai dan nay do thang nao su dung
    protected float angleShoot; //Goc ban cua vien dan

    public float AngelShoot
    {
        get { return angleShoot; }
        set { angleShoot = value; }
    }

    public float Damge
    {
        get { return damge; }
        set { damge = value; }
    }

    public BaseBulletType BulletType
    {
        get { return bulletType; }
        set { bulletType = value; }
    }

    public BaseObjectType ObjectUseType
    {
        get { return objectUseType; }
        set { objectUseType = value; }
    }

    public override void InitObject()
    {
        base.InitObject();
        gameObjectType = BaseObjectType.OB_GUN;
        positionBegin = transform.position;
        //direction = BaseDirectionType.UP;
    }

    public override void Move()
    {
        vx += accelerationNormalX * Time.deltaTime;
        vy += accelerationNormalY * Time.deltaTime;

        switch (direction)
        {
            case BaseDirectionType.UP:
                positionBegin.y += vy * Time.deltaTime;
                break;
            case BaseDirectionType.LEFT:
                positionBegin.x -= vx * Time.deltaTime;
                break;
            case BaseDirectionType.RIGHT:
                positionBegin.x += vx * Time.deltaTime;
                break;
            case BaseDirectionType.DOWN:
                positionBegin.y -= vy * Time.deltaTime;
                break;
            case BaseDirectionType.LEFT_UP:
                positionBegin.x -= vx * Time.deltaTime;
                positionBegin.y += vy * Time.deltaTime;
                break;
            case BaseDirectionType.RIGHT_UP:
                positionBegin.x += vx * Time.deltaTime;
                positionBegin.y += vy * Time.deltaTime;
                break;
            case BaseDirectionType.LEFT_DOWN:
                positionBegin.x -= vx * Time.deltaTime;
                positionBegin.y -= vy * Time.deltaTime;
                break;
            case BaseDirectionType.RIGHT_DOWN:
                positionBegin.x += vx * Time.deltaTime;
                positionBegin.y -= vy * Time.deltaTime;
                break;
            case BaseDirectionType.NONE:
                break;
            default:
                break;
        }

        transform.position = positionBegin;
    }

    public override void UpdateObject()
    {
        base.UpdateObject();
        this.Move();
        KillEnemies();
    }

    public abstract void KillEnemies();

    public override void DestroyObject()
    {
        //Dua bullet vao lai trong Pool
        if (positionBegin.y <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y || 
            positionBegin.y >= Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0.0f)).y ||
            positionBegin.x <= Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x ||
            positionBegin.x >= Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0.0f)).x
            )
        {
            PoolCustomize.Instance.HideBaseObject(gameObject, "Bullet");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "GroundBox")
        {
            PoolCustomize.Instance.HideBaseObject(gameObject, "Bullet");
        }
        else if (other.tag == "Enemy")
        {
            BaseEnemyObject baseEnemy = other.gameObject.GetComponent<BaseEnemyObject>();
            baseEnemy.ReceiveDamge(damge);
            PoolCustomize.Instance.HideBaseObject(gameObject, "Bullet");
        }
    }
}
