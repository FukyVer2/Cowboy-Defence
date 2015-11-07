using UnityEngine;
using System.Collections;

public abstract class BaseBulletObject : BaseMoveObject {

    protected float damge; // Damge cua dan
    protected BaseBulletType bulletType; //Thong so chi loai dan
    protected BaseObjectType objectUseType; //Thong so chi loai dan nay do thang nao su dung
    protected float angleShoot; //Goc ban cua vien dan

    protected bool isStun; //Dan nay co stun
    protected bool isCritDamge; // Dan nay co crit damge hay ko
    protected float critDamge; //Damge tang them bao nhieu %

    public bool IsStun
    {
        get { return isStun; }
        set { isStun = value; }
    }


    public bool IsCritDamge
    {
        get { return isCritDamge; }
        set { isCritDamge = value; }
    }


    public float CritDamge
    {
        get { return critDamge; }
        set { critDamge = value; }
    }

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
        DestroyObject();
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
            // spawn partical 
            ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_EXPLOSION_C_E_B, transform.position);
            
            BaseEnemyObject baseEnemy = other.gameObject.GetComponent<BaseEnemyObject>();
            if (baseEnemy.healthPoint > 0)
            {
                if (IsCritDamge)
                {
                    baseEnemy.ReceiveDamge(damge + (int)(damge * CritDamge));
                }
                else
                {
                    baseEnemy.ReceiveDamge(damge);
                }
                PoolCustomize.Instance.HideBaseObject(gameObject, "Bullet");
            }

            if (bulletType == BaseBulletType.BL_SLOW || IsStun)
            {
                baseEnemy.effectRenderer.AddStatModifier(BaseStatModifierType.BSM_SLOW, 2.5f, 0.2f);
                baseEnemy.SetColor(new Color(0, 0.5f, 0));
            }
            else
            {
                if (!baseEnemy.effectRenderer.Contains(BaseStatModifierType.BSM_SLOW))
                {
                    baseEnemy.SetColor(new Color(0.8f, 0.2f, 0.2f, 1f));
                    baseEnemy.ResetColor(0.5f);
                }
            }
        }
        else if(other.tag =="Boss")
        {
            // instance partical
            ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_EXPLOSION_C_E_B, transform.position);
            //tru mau
            BaseBossObject baseBoss = other.gameObject.GetComponent<BaseBossObject>();
            if(baseBoss.healthPoint >0)
            {
                baseBoss.ReceiveDamge(this.damge);
                PoolCustomize.Instance.HideBaseObject(gameObject,"Bullet");
            }

        }
    }
}
