using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter(){
        base.Enter();
    }
    public override void Exit(){
        base.Exit();
        player.SetVelocity(0, player.rb.linearVelocity.y);
    }
    public override void Update(){
        base.Update();

        if (player.CheckIfTouchingWall()){
            stateMachine.ChangeState(player.wallSlideState);
        }
        
        if (player.rb.linearVelocity.y == 0){
            stateMachine.ChangeState(player.idleState);
        }
        if (xInput != 0){
            player.SetVelocity(player.normMoveSpeed * xInput * .75f, player.rb.linearVelocity.y);
        }
    }
}
