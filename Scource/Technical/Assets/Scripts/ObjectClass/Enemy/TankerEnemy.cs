using UnityEngine;
using System.Collections;

public class TankerEnemy : BaseEnemyObject {


    public override void KillPlayer()
    {
        throw new System.NotImplementedException();
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

    public override void ReceiveDamge(float damge)
    {
        this.healthPoint -= damge;
    }

    public void AllowAttack()
    {
        stateMachine.ChangeState(BaseStateType.ES_ATTACK);
    }

    public void AllowIdle()
    {
        stateMachine.ChangeState(BaseStateType.ES_ATTACK);
    }

    public override void Attack()
    {
        if (healthPoint <= 0)
        {
            stateMachine.ChangeState(BaseStateType.ES_DIE);
        }
        else
        {
            Invoke("AllowIdle", timeAttack);
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
                Invoke("AllowAttack", timeWaitAttack);
            }
            else
            {
                stateMachine.ChangeState(BaseStateType.ES_RUN);
            }
        }
    }

    public override void Die()
    {
        DestroyObject();
    }

    public override void Run()
    {
        if (healthPoint <= 0)
        {
            stateMachine.ChangeState(BaseStateType.ES_DIE);
        }
        else
        {
            Move();
        }
    }

    public void GiveDamge()
    {
        Debug.Log("Cho damge");
        if (wallTarget != null)
        {
            if (wallTarget.healthPoint > 0)
            {
                wallTarget.ReceiveDamge(this.damge);

                if (wallTarget.healthPoint <= 0)
                {
                    Destroy(wallTarget.gameObject);
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
        base.DestroyObject();
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            
            stateMachine.ChangeState(BaseStateType.ES_ATTACK);
            this.attacking = true;
            BaseWallObject baseWallObject = other.gameObject.GetComponent<BaseWallObject>();
            wallTarget = baseWallObject;
            //baseWallObject.ReceiveDamge(this.damge);
        }
    }
}
