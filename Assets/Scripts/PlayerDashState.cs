using UnityEngine;

public class PlayerDashState : PlayerState
{

    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, player.rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashSpeed * (player.facingRight ? 1 : -1), 0);
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (!player.CheckIfGrounded() && player.CheckIfTouchingWall()){
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
