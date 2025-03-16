using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private Enemy_Skeleton skeleton;

    public SkeletonAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
        skeleton = _enemy as Enemy_Skeleton;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
        skeleton.lastAttackTime = Time.time;
    }

    public override void Update()

    {
        base.Update();
        if (triggerCalled){
            stateMachine.ChangeState(skeleton.battleState);
        }
    }
}

