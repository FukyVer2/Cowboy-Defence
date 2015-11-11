using UnityEngine;
using System.Collections;

public class TankerEnemy : BaseEnemyObject {

    public Transform hitPosition;

    public override void KillPlayer()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        vx += accelerationNormalX * Time.deltaTime;
        vy += accelerationNormalY * Time.deltaTime;
        //
        if (effectRenderer.Contains(BaseStatModifierType.BSM_SLOW))
        {
            if (!effectRenderer.RemoveStatModifier(BaseStatModifierType.BSM_SLOW))
            {
                switch (direction)
                {
                    case BaseDirectionType.UP:
                        positionBegin.y += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.LEFT:
                        positionBegin.x -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vx * Time.deltaTime;
                        break;
                    case BaseDirectionType.RIGHT:
                        positionBegin.x += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vx * Time.deltaTime;
                        break;
                    case BaseDirectionType.DOWN:
                        positionBegin.y -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.LEFT_UP:
                        positionBegin.x -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vx * Time.deltaTime;
                        positionBegin.y += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.RIGHT_UP:
                        positionBegin.x += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vx * Time.deltaTime;
                        positionBegin.y += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.LEFT_DOWN:
                        positionBegin.x -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vx * Time.deltaTime;
                        positionBegin.y -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.RIGHT_DOWN:
                        positionBegin.x += effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vx * Time.deltaTime;
                        positionBegin.y -= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW) * vy * Time.deltaTime;
                        break;
                    case BaseDirectionType.NONE:
                        break;
                    default:
                        break;
                }
                //vx *= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW);
                //vy *= effectRenderer.GetStatModifier(BaseStatModifierType.BSM_SLOW);
                //Remove khi het thoi gian
            }
            else
            {
                this.SetColor(new Color(255, 255, 255));
            }
            //heffectRenderer.RemoveStatModifier(BaseStatModifierType.BSM_SLOW);
        }
        else
        {
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
        }
        transform.position = positionBegin;
    }

    public override void ReceiveDamge(float damge)
    {
        this.healthPoint -=  damge;
        this.hpView.UpdateHealthPoint(healthPoint / healthBegin);
        try
        {
            //
            GameObject numberObj = ManagerObject.Instance.SpawnObject(BaseObjectType.OU_NUMBER, hitPosition.position, "Item");
            Number number = numberObj.GetComponent<Number>();
            number.Reset();
            number.SetValue(damge.ToString());
        }
        catch
        {

        }
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
        Debug.Log("Cho damge");
        if (wallTarget != null)
        {
            if (wallTarget.healthPoint > 0)
            {
                wallTarget.ReceiveDamge(this.damge);

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
        base.DestroyObject();
        ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_ENEMY_DIE, transform.position);
        PoolCustomize.Instance.HideBaseObject(gameObject, "Enemy");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            //stateMachine.ChangeState(BaseStateType.ES_ATTACK);
            this.attacking = true;
            BaseWallObject baseWallObject = other.gameObject.GetComponent<BaseWallObject>();
            wallTarget = baseWallObject;
            //baseWallObject.ReceiveDamge(this.damge);
        }
        if(other.gameObject.tag == "Bom")
        {
            PoolCustomize.Instance.HideBaseObject(other.gameObject, "Item", 0.3f);
            float timeRandom = Random.Range(0.2f, 0.8f);
            Invoke("InstantialeParticalExplosion", timeRandom);
            Item1 itemBom = other.gameObject.GetComponent<Item1>();
            ReceiveDamge(itemBom.damge);
            //Debug.Log(timeRandom);
        }
    }

    public void InstantialeParticalExplosion()
    {
        //AudioManager.Instance.Play(BaseAudioType.BA_ENEMY_BOM_ITEM,false);
        AudioSource.PlayClipAtPoint(AudioManager.Instance.GetSoundByType(BaseAudioType.BA_ENEMY_BOM_ITEM), transform.position);
        ManagerObject.Instance.SpawnPartical(BaseObjectType.OBP_BOM_ITEM_EXPLOSION, transform.position);
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            stateMachine.ChangeState(BaseStateType.ES_RUN);
            this.attacking = false;
            wallTarget = null;
            //baseWallObject.ReceiveDamge(this.damge);
        }
    }
}
