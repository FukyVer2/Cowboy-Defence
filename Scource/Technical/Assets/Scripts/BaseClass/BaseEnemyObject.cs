using UnityEngine;
using System.Collections;

public abstract class BaseEnemyObject : BaseMoveObject
{
    public float damge; // Thong so chi luong damge cua moi con Enemy
    public float healthPoint; //Thong so chi luong mau cua enemy
    public BaseStateType stateCurrent; //Trang thai hien tai cua Enemy
    public StateMachine stateMachine; //Day la may chuyen trang thai cua thang enemy
    public Animator baseEnemyAnimator; //Quan ly trang thai cua enemy

    public bool attacking;  //dang danh
    public float timeAttack; //thoi gian danh
    public float timeWaitAttack; //thoi gian cho chuyen sang trang thai danh
    public BaseWallObject wallTarget; //Wall dang danh

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

