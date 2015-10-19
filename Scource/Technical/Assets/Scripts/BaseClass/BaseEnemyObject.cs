using UnityEngine;
using System.Collections;

public abstract class BaseEnemyObject : BaseMoveObject
{
    public float damge; // Thong so chi luong damge cua moi con Enemy
    public float healthPoint; //Thong so chi luong mau cua enemy
    public BaseStateType stateCurrent; //Trang thai hien tai cua Enemy
    public StateMachine stateMachine; //Day la may chuyen trang thai cua thang enemy

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
        stateMachine.ChangeState(BaseStateType.ES_RUN);
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
        
    }

    public void UpdateState()
    {
       
    }

    public void EndState()
    {
        
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

    }

    public void UpdateState()
    {

    }

    public void EndState()
    {

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

    }

    public void UpdateState()
    {
        enemyObject.Move();
    }

    public void EndState()
    {

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

    }

    public void UpdateState()
    {

    }

    public void EndState()
    {

    }
}

