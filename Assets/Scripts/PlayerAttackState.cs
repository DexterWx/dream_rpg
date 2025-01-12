using UnityEngine;

public class PlayerAttackState : PlayerState
{

    public int comboCounter = 0;
    private float lastTimeAttacked;
    private float comboWindow = 2;
    private int attackDir = 0;

    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        
    }

    public override void Enter(){
        base.Enter();
        if (comboCounter >= 3 || Time.time >= lastTimeAttacked + comboWindow){
            comboCounter = 0;
        }
        player.anim.SetInteger("ComboCount", comboCounter);
        stateTimer = .1f;

        attackDir = player.facingRight ? 1 : -1;
        if (xInput != 0){
            attackDir = xInput < 0 ? -1 : 1;
        }
        
        player.SetVelocity(
            player.attackMovement[comboCounter].x * attackDir,
            player.attackMovement[comboCounter].y
        );
    }
    public override void Exit(){
        base.Exit();
        player.StartCoroutine(player.BusyFor(.15f));
        lastTimeAttacked = Time.time;
        comboCounter += 1;
    }
    public override void Update(){
        base.Update();

        if (stateTimer < 0){
            player.SetVelocity(0, 0);
        }
        if (triggerCalled){
            stateMachine.ChangeState(player.idleState);
        }
    }
}
