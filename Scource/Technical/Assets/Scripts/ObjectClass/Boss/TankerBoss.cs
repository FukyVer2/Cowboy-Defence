using UnityEngine;
using System.Collections;

public class TankerBoss : BaseEnemyObject {
    // thuộc tính riêng của boss
    public int countReceiveBullet =0; // so vien dan ma no bi trung

    public GameObject objParticalSummoned;
    public ParticleSystem objParticalAttack;

    public float minXRandom;
    public float maxXRandom;

    public override void InitObject()
    {
        base.InitObject();
        gameObjectType = BaseObjectType.OB_ENEMY;
        positionBegin = transform.position;
        InitStateMachine();
        objParticalAttack.Stop();
        //stateMachine.ChangeState(BaseStateType.ES_IDLE);
        healthPoint = healthBegin;
        this.hpView.UpdateHealthPoint(healthPoint / healthBegin);
        isAlive = true;
        isAttack = false;
        isIdle = false;
    }

    // Set chuyen boss tu xuat hien sang idle
    public void SetFrameFinalSummoned()
    {
        stateMachine.ChangeState(BaseStateType.ES_IDLE);
        objParticalSummoned.SetActive(false);
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

        base.UpdateObject();
    }
    public override void ReceiveDamge(float damge)
    {
        this.healthPoint -= damge;
    }

    public void AllowAttack()
    {
        isIdle = false;
        if (isAlive)
        {
            stateMachine.ChangeState(BaseStateType.ES_ATTACK);
        }
    }

    public void AllowIdle()
    {
        isAttack = false;
        if (isAlive)
        {
            stateMachine.ChangeState(BaseStateType.ES_IDLE);
        }
    }

    public override void Attack()
    {
        if (healthPoint <= 0)
        {
            stateMachine.ChangeState(BaseStateType.ES_DIE);
        }
        else
        {
            if (!isAttack)
            {
                isAttack = true;
                Invoke("AllowIdle", timeAttack);
            }
        }
    }

    public override void Idle()
    {
        if (healthPoint <= 0)
        {
            stateMachine.ChangeState(BaseStateType.ES_DIE);
        }
        else
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
                stateMachine.ChangeState(BaseStateType.ES_RUN);
            }
        }
    }

    public override void Die()
    {
        if (isAlive)
        {
            isAlive = false;
            Invoke("DestroyObject", 0.2f);
        }
    }

    public override void Run()
    {
        if (healthPoint <= 0)
        {
            stateMachine.ChangeState(BaseStateType.ES_DIE);
        }
        else
        {
            if (attacking)
            {
                stateMachine.ChangeState(BaseStateType.ES_IDLE);
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
                    stateMachine.ChangeState(BaseStateType.ES_RUN);
                }
            }
        }
        else
        {
            wallTarget = null;
            attacking = false;
            stateMachine.ChangeState(BaseStateType.ES_RUN);
        }
    }

    public override void DestroyObject()
    {
#if UNITY_EDITOR
        Debug.Log("can't destroyobject!");
#endif
        //base.DestroyObject();
        //ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_ENEMY_DIE, transform.position);
        //PoolCustomize.Instance.HideBaseObject(gameObject, "Enemy");
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
      
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            stateMachine.ChangeState(BaseStateType.ES_RUN);
            this.attacking = false;
            wallTarget = null;
        }
    }
}
