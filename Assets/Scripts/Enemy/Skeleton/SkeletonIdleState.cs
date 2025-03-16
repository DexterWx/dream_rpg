using UnityEngine;

public class SkeletonIdleState : SkeletonGroudState
{
    private Enemy_Skeleton skeleton;
    public SkeletonIdleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
        skeleton = _enemy as Enemy_Skeleton;
    }


    public override void Enter()
    {
        base.Enter();
        stateTimer = skeleton.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer > 0){
            stateTimer -= Time.deltaTime;
        }
        else{
            stateMachine.ChangeState(skeleton.moveState);
        }

    }
}

