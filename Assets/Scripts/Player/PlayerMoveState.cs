using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter(){
        base.Enter();
    }
    public override void Exit(){
        base.Exit();
    }
    public override void Update(){
        base.Update();

        player.SetVelocity(xInput * player.normMoveSpeed, player.rb.linearVelocity.y);

        if (xInput == 0 || player.CheckIfTouchingWall()){
            stateMachine.ChangeState(player.idleState);
        }
    }
    
}
