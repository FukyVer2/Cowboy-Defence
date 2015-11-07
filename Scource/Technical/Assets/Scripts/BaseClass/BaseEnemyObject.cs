using UnityEngine;
using System.Collections;

public abstract class BaseEnemyObject : BaseMoveObject
{
    public float damge; // Thong so chi luong damge cua moi con Enemy
    public float healthPoint; //Thong so chi luong mau cua enemy
    public float healthBegin; //Thong so mau ban dau
    //public BaseStateType stateCurrent; //Trang thai hien tai cua Enemy
    public StateMachine stateMachine; //Day la may chuyen trang thai cua thang enemy
    public Animator baseEnemyAnimator; //Quan ly trang thai cua enemy
    public bool isAlive;                // kiem tra song or chet
    public bool attacking;  //dang danh
    protected bool isAttack;
    protected bool isIdle;
    public float timeAttack; //thoi gian danh
    public float timeWaitAttack; //thoi gian cho chuyen sang trang thai danh
    public BaseWallObject wallTarget; //Wall dang danh
    //
    public float coin; //Khi con nay bi danh se roi ra bao nhieu tien
    public EffectRenderer effectRenderer; //Quan ly viec sinh ra effect
    //
    public UIHealth hpView; //Hien thi thanh mau
    //
    public SpriteRenderer spriteRenderer;
    public Color colorOld; //Mau truoc do cua sprite

    public virtual void SetColor(Color _color)
    {
        spriteRenderer.color = _color;
    }

    public virtual void ResetColor()
    {
        if (!effectRenderer.Contains(BaseStatModifierType.BSM_SLOW))
        {
            this.SetColor(new Color(255, 255, 255));
        }
    }

    public virtual void ResetColor(float _time)
    {

        Invoke("ResetColor", _time);
    }

    public virtual void InitStateMachine()
    {
        stateMachine = new StateMachine();
        stateMachine.AddStateAction(BaseStateType.ES_IDLE, new EnemyIdleState(this));
        stateMachine.AddStateAction(BaseStateType.ES_RUN, new EnemyRunState(this));
        stateMachine.AddStateAction(BaseStateType.ES_ATTACK, new EnemyAttackState(this));
        stateMachine.AddStateAction(BaseStateType.ES_DIE, new EnemyDieState(this));
        
    }

    public override void InitObject()
    {
        base.InitObject();
        gameObjectType = BaseObjectType.OB_ENEMY;
        positionBegin = transform.position;
        InitStateMachine();
        stateMachine.ChangeState(BaseStateType.ES_IDLE);
        healthPoint = healthBegin;
        this.hpView.UpdateHealthPoint(healthPoint / healthBegin);
        isAlive = true;
        isAttack = false;
        isIdle = false;
    }

    public override void ResetValueOfAvariable()
    {
        base.ResetValueOfAvariable();
        positionBegin = transform.position;
        if(stateMachine != null)
            stateMachine.ChangeState(BaseStateType.ES_IDLE);
        healthPoint = healthBegin;
        this.hpView.UpdateHealthPoint(BaseUtilExtentions.Instance.RoundToFloat(healthPoint / healthBegin, 2));
        attacking = false;
        isAlive = true;
        isAttack = false;
        isIdle = false;
    }

    public override void UpdateObject()
    {
        base.UpdateObject();
        stateMachine.MachineStateUpdate();
    }

    public abstract void KillPlayer();

    public override void Move()
    {

    }

    public override void DestroyObject()
    {
        //Huy doi tuong khoi danh sach spawn
        if (SpawnEnemyController.Instance.enemyInScreen.Contains(this))
        {
            SpawnEnemyController.Instance.enemyInScreen.Remove(this);
        }
        GiveCoin();
    }

    public void GiveCoin()
    {
        Coin coinObj;
        GameObject coinObject;
        for (int i = 0; i < coin / Coin.COIN_POINT; i++)
        {
            coinObject = ManagerObject.Instance.SpawnItem(BaseObjectType.OU_COIN, transform.position);
            if (coinObject != null)
            {
                coinObj = coinObject.GetComponent<Coin>();
                coinObj.PositionMoveTo = GameController.Instance.scoreView.transform.position;
                coinObj.ResetValueOfAvariable();
                coinObj.vx = Random.Range(-coinObj.velocityNormalX, coinObj.velocityNormalX);
                //coinObj.velocityNormalY = Random.Range(coinObj.velocityNormalY, coinObj.velocityNormalY + 1);
            }
        }

        GameController.Instance.UpdatePlayerScore(coin);
    }

    public abstract void ReceiveDamge(float damge);

    public abstract void Run();

    public abstract void Attack();

    public abstract void Idle();

    public abstract void Die();
}

class EnemyIdleState : IState
{
    BaseEnemyObject enemyObject;
    public EnemyIdleState(BaseEnemyObject _enemyObject)
    {
        enemyObject = _enemyObject;
    }

    public void BeginState()
    {
        enemyObject.baseEnemyAnimator.SetBool("IsIdle", true);
    }

    public void UpdateState()
    {
        enemyObject.Idle();
    }

    public void EndState()
    {
        enemyObject.baseEnemyAnimator.SetBool("IsIdle", false);
    }
}

class EnemyAttackState : IState
{
    BaseEnemyObject enemyObject;
    public EnemyAttackState(BaseEnemyObject _enemyObject)
    {
        enemyObject = _enemyObject;
    }

    public void BeginState()
    {
        enemyObject.baseEnemyAnimator.SetBool("IsAttack", true);
    }

    public void UpdateState()
    {
        enemyObject.Attack();
    }

    public void EndState()
    {
        enemyObject.baseEnemyAnimator.SetBool("IsAttack", false);
    }
}

class EnemyRunState : IState
{
    BaseEnemyObject enemyObject;
    public EnemyRunState(BaseEnemyObject _enemyObject)
    {
        enemyObject = _enemyObject;
    }

    public void BeginState()
    {
        enemyObject.baseEnemyAnimator.SetBool("IsRun", true);
    }

    public void UpdateState()
    {
        enemyObject.Run();
    }

    public void EndState()
    {
        enemyObject.baseEnemyAnimator.SetBool("IsRun", false);
    }
}

class EnemyDieState : IState
{
    BaseEnemyObject enemyObject;
    public EnemyDieState(BaseEnemyObject _enemyObject)
    {
        enemyObject = _enemyObject;
    }

    public void BeginState()
    {
        enemyObject.baseEnemyAnimator.SetBool("IsDie", true);
    }

    public void UpdateState()
    {
        enemyObject.Die();
    }

    public void EndState()
    {
        enemyObject.baseEnemyAnimator.SetBool("IsDie", false);
    }
}

//class EnemySummonedState : IState
//{
//    BaseEnemyObject enemyObject;
//    public EnemySummonedState(BaseEnemyObject _enemyObject)
//    {
//        enemyObject = _enemyObject;
//    }
//    public void beginState()
//    {
//        enemyObject.baseEnemyAnimator.SetTrigger("IsSummoned");
//    }
//}
