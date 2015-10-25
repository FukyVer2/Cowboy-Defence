using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
public abstract class BaseBossObject : BaseMoveObject
{
    public float damge; // Thong so chi luong damge cua moi con Enemy
    public float healthPoint; //Thong so chi luong mau cua enemy
    public float healthBegin; //Thong so mau ban dau
    public StateMachine stateMachine; //Day la may chuyen trang thai cua thang enemy
    public Animator baseEnemyAnimator; //Quan ly trang thai cua enemy
    public bool isAlive;                // kiem tra song or chet
    public bool attacking;  //dang danh
    protected bool isAttack;
    protected bool isIdle;
    public float timeAttack; //thoi gian danh
    public float timeWaitAttack; //thoi gian cho chuyen sang trang thai danh
    public BaseWallObject wallTarget; //Wall dang danh

    public UIHealth hpView; //Hien thi thanh mau

    public override void InitObject()
    {
        base.InitObject();
        this.gameObjectType = BaseObjectType.OB_BOSS_TANKER;
        isAlive = true;
        InitStateMachine();
        positionBegin = transform.position;
        healthPoint = healthBegin;
        this.hpView.UpdateHealthPoint(healthPoint / healthBegin);
        isAttack = false;
        isIdle = false;
    }

    public virtual void InitStateMachine()
    {
        stateMachine = new StateMachine();
        //stateMachine.AddStateAction(BaseStateType.BS_SUMMON, new BossSummonedState(this));
        stateMachine.AddStateAction(BaseStateType.BS_IDLE, new BossIdleState(this));
        stateMachine.AddStateAction(BaseStateType.BS_RUN, new BossRunState(this));
        stateMachine.AddStateAction(BaseStateType.BS_DIE, new BossDieState(this));
        stateMachine.AddStateAction(BaseStateType.BS_ATTACK, new BossAttackState(this));
    }
    public override void Move()
    {
        
    }
    public override void UpdateObject()
    {
        stateMachine.MachineStateUpdate();
        UpdateDie();
        base.UpdateObject();
    }
    public void UpdateDie()
    {
        if(healthPoint <=0)
        {
            stateMachine.ChangeState(BaseStateType.BS_DIE);
        }
    }
    public override void ResetValueOfAvariable()
    {
        base.ResetValueOfAvariable();
        //positionBegin = transform.position;
        //if (stateMachine != null)
        //    stateMachine.ChangeState(BaseStateType.ES_IDLE);
        //healthPoint = healthBegin;
        //this.hpView.UpdateHealthPoint(BaseUtilExtentions.Instance.RoundToFloat(healthPoint / healthBegin, 2));
        //attacking = false;
        //isAlive = true;
        //isAttack = false;
        //isIdle = false;
    }
    public abstract void KillPlayer();
    public abstract void ReceiveDamge(float damge);
    public override void DestroyObject()
    {
        
    }
    public virtual void Idle()
    {

    }
    public virtual void Run()
    {

    }
    public virtual void Die()
    {

    }
    public virtual void Attack()
    {

    }
}

public class BossSummonedState:IState
{
    BaseBossObject baseBossObject;
    public BossSummonedState(BaseBossObject _BaseBossObject)
    {
        this.baseBossObject = _BaseBossObject;
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
public class BossIdleState:IState
{
    BaseBossObject baseBossObject;
    public BossIdleState(BaseBossObject _baseBossObject)
    {
        this.baseBossObject = _baseBossObject;
    }
    public void BeginState()
    {
        baseBossObject.baseEnemyAnimator.SetBool("IsIdle",true);
    }

    public void UpdateState()
    {
        baseBossObject.Idle();
    }
    public void EndState()
    {
        baseBossObject.baseEnemyAnimator.SetBool("IsIdle", false);
    }
}

public class BossRunState:IState
{
    BaseBossObject baseBosssObject;
    public BossRunState(BaseBossObject _baseBossObject)
    {
        this.baseBosssObject = _baseBossObject;
    }
    public void BeginState()
    {
        baseBosssObject.baseEnemyAnimator.SetBool("IsRun",true);
    }

    public void UpdateState()
    {
        baseBosssObject.Run();
    }
    public void EndState()
    {
        baseBosssObject.baseEnemyAnimator.SetBool("IsRun", false);
    }
}
public class BossDieState : IState
{
    BaseBossObject baseBosssObject;
    public BossDieState(BaseBossObject _baseBossObject)
    {
        this.baseBosssObject = _baseBossObject;
    }
    public void BeginState()
    {
        baseBosssObject.baseEnemyAnimator.SetBool("IsDie", true);
    }

    public void UpdateState()
    {
        baseBosssObject.Die();
    }
    public void EndState()
    {
        baseBosssObject.baseEnemyAnimator.SetBool("IsDie", false);
    }
}

public class BossAttackState : IState
{
    BaseBossObject baseBosssObject;
    public BossAttackState(BaseBossObject _baseBossObject)
    {
        this.baseBosssObject = _baseBossObject;
    }
    public void BeginState()
    {
        baseBosssObject.baseEnemyAnimator.SetBool("IsAttack", true);
    }

    public void UpdateState()
    {
        baseBosssObject.Attack();
    }
    public void EndState()
    {
        baseBosssObject.baseEnemyAnimator.SetBool("IsAttack", false);
    }
}