using UnityEngine;
using System.Collections;

public class EnemySupport : BaseEnemyObject {

    public override void Attack()
    {

        stateMachine.ChangeState(BaseStateType.ES_ATTACK);

    }
    public override void Die()
    {

        stateMachine.ChangeState(BaseStateType.ES_DIE);

    }
    public override void Idle()
    {

        stateMachine.ChangeState(BaseStateType.ES_IDLE);
    }
    public override void KillPlayer()
    {

    }
    public override void ReceiveDamge(float damge)
    {

    }
    public override void Run()
    {

    }
    public override void UpdateObject()
    {
        base.UpdateObject();
    }
    public override void Move()
    {
        //Debug.Log("move");
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
        base.Move();
    }
}
