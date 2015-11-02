using UnityEngine;
using System.Collections;

public class TankerBoss : BaseBossObject {
    // thuộc tính riêng của boss
    public int countReceiveBullet =0; // so vien dan ma no bi trung

    public ParticleSystem objParticalSummoned;
    public ParticleSystem objParticalAttack;

    public float minXRandom;
    public float maxXRandom;

    public SpriteRenderer spriteRender;
    public BoxCollider2D box;

    public Transform hitPosition;
    public GameObject obj_HP;
    public override void InitObject()
    {
        base.InitObject();
        objParticalAttack.Stop();
    }

    // Set chuyen boss tu xuat hien sang idle
    public void SetFrameFinalSummoned()
    {
        stateMachine.ChangeState(BaseStateType.BS_IDLE);
        objParticalSummoned.Stop();
    }
    // cho damge
    public override void KillPlayer()
    {
        //throw new System.NotImplementedException();
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
        UpdateAI();
        base.UpdateObject();
    }
    public void UpdateAI()
    {
        if(countReceiveBullet > 20 && attacking == false)
        {
            countReceiveBullet = 0;
            spriteRender.enabled = false;
            box.enabled = false;
            obj_HP.SetActive(false);
            Invoke("AllowInvi",1.5f);
        }
    }
    public void AllowInvi()
    {
        positionBegin = new Vector3(Random.Range(minXRandom, maxXRandom), transform.position.y, transform.position.z);
        spriteRender.enabled = true;
        obj_HP.SetActive(true);
        box.enabled = true;
    }
    public override void ReceiveDamge(float damge)
    {
        this.healthPoint -= damge;
        this.hpView.UpdateHealthPoint(healthPoint / healthBegin);
        //
        GameObject numberObj = ManagerObject.Instance.SpawnObject(BaseObjectType.OU_NUMBER, hitPosition.position, "Item");
        Number number = numberObj.GetComponent<Number>();
        number.Reset();
        number.SetValue(damge.ToString());
    }

    public void AllowAttack()
    {
        isIdle = false;
        if (isAlive)
        {
            stateMachine.ChangeState(BaseStateType.BS_ATTACK);
        }
    }

    public void AllowIdle()
    {
        isAttack = false;
        if (isAlive)
        {
            stateMachine.ChangeState(BaseStateType.BS_IDLE);
        }
    }

    public override void Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            Invoke("AllowIdle", timeAttack);
        }
    }

    public override void Idle()
    {
        if (attacking)
        {
            if (!isIdle)
            {
                isIdle = true;
                Invoke("AllowAttack", timeWaitAttack);

            }
        }
        else
        {
            stateMachine.ChangeState(BaseStateType.BS_RUN);
        }
    }

    public override void Die()
    {
        if (isAlive)
        {
            isAlive = false;
            Invoke("DestroyObject", 1f);
        }
    }

    public override void Run()
    {
        if (healthPoint <= 0)
        {
            stateMachine.ChangeState(BaseStateType.BS_DIE);
        }
        else
        {
            if (attacking)
            {
                stateMachine.ChangeState(BaseStateType.BS_IDLE);
            }
            else
                Move();
        }
    }

    public void GiveDamge()
    {
       // Debug.Log("Cho damge");
        if (wallTarget != null)
        {
            if (wallTarget.healthPoint > 0)
            {
                wallTarget.ReceiveDamge(this.damge);
                // instanitial partical bum
                objParticalAttack.transform.position = wallTarget.transform.position;
                objParticalAttack.Play();
                if (wallTarget.healthPoint <= 0)
                {
                    wallTarget.DestroyObject();
                    //Destroy(wallTarget.gameObject);
                    wallTarget = null;
                    attacking = false;
                    stateMachine.ChangeState(BaseStateType.BS_RUN);
                }
            }
        }
        else
        {
            wallTarget = null;
            attacking = false;
            stateMachine.ChangeState(BaseStateType.BS_RUN);
        }
    }

    public override void DestroyObject()
    {
#if UNITY_EDITOR
        Debug.Log("can't destroyobject for code!");
#endif
        base.DestroyObject();
        ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_ENEMY_DIE, transform.position);
        PoolCustomize.Instance.HideBaseObject(gameObject, "Enemy");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            //stateMachine.ChangeState(BaseStateType.ES_IDLE);
            this.attacking = true;
            BaseWallObject baseWallObjectScripts = other.GetComponent<BaseWallObject>();
            this.wallTarget = baseWallObjectScripts;
        }
        if(other.tag =="Bullet")
        {
            countReceiveBullet++;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            stateMachine.ChangeState(BaseStateType.BS_IDLE);
            this.attacking = false;
            wallTarget = null;
        }
    }
}
