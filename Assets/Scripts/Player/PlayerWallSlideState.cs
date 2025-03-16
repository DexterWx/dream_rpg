using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
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
        player.SetVelocity(player.rb.linearVelocity.x, player.rb.linearVelocity.y * .85f);
        if (xInput != 0 && player.facingRight != (xInput > 0)){
            stateMachine.ChangeState(player.idleState);
        }
        if (player.CheckIfGrounded()){
            stateMachine.ChangeState(player.idleState);
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            stateMachine.ChangeState(player.wallJumpState);
        }
    }
}
