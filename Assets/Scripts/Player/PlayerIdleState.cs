using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter(){
        base.Enter();
        player.SetVelocity(0, 0);
    }
    public override void Exit(){
        base.Exit();
    }
    public override void Update(){
        base.Update();
        if (xInput != 0 && !player.isBusy){
            if (player.CheckIfTouchingWall()){
                if (player.facingRight != (xInput > 0)) {
                    stateMachine.ChangeState(player.moveState);
                }
            }
            else{
                stateMachine.ChangeState(player.moveState);
            }
        }
    }
}
