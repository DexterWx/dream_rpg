using UnityEngine;

public class SkeletonMoveState : SkeletonGroudState
{
    private Enemy_Skeleton skeleton;
    public SkeletonMoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
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
    }

    public override void Update()
    {
        base.Update();
        skeleton.SetVelocity(skeleton.moveSpeed * (skeleton.facingRight ? 1 : -1), skeleton.rb.linearVelocity.y);
        if (skeleton.CheckIfTouchingWall() || !skeleton.CheckIfGrounded()){

            skeleton.Flip();
            skeleton.stateMachine.ChangeState(skeleton.idleState);
        }


    }


}

