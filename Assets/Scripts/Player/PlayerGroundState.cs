using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (!player.CheckIfGrounded()){
            stateMachine.ChangeState(player.airState);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.CheckIfGrounded()){
            stateMachine.ChangeState(player.jumpState);
        }

        if (Input.GetKey(KeyCode.Mouse0)){
            stateMachine.ChangeState(player.attackState);
        }

        if (Input.GetKeyDown(KeyCode.Q)){
            stateMachine.ChangeState(player.counterAttackState);
        }
    }

}
