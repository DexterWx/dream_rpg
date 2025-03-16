using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    #region States
    public SkeletonIdleState idleState {get; private set;}
    public SkeletonMoveState moveState {get; private set;}
    public SkeletonBattleState battleState {get; private set;}
    public SkeletonAttackState attackState {get; private set;}
    public SkeletonStunState stunState {get; private set;}
    #endregion

    [Header("Movement Details")]
    public float moveSpeed;
    public float idleTime;



    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "Idle");
        moveState = new SkeletonMoveState(this, stateMachine, "Move");
        battleState = new SkeletonBattleState(this, stateMachine, "Move");
        attackState = new SkeletonAttackState(this, stateMachine, "Attack");
        stunState = new SkeletonStunState(this, stateMachine, "Stun");

    }


    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanBeStunned(){
        if (base.CanBeStunned()){
            stateMachine.ChangeState(stunState);
            return true;
        }
        else{
            return false;
        }
    }

    

}
