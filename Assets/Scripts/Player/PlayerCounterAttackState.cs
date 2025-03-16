using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter(){
        stateTimer = player.counterDuration;
        base.Enter();
    }

    public override void Exit(){
        base.Exit();
    }

    public override void Update(){
        base.Update();
        player.rb.linearVelocity = new Vector2(0, 0);

        if (stateTimer <= 0 || triggerCalled){
            stateMachine.ChangeState(player.idleState);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackRadius);
        foreach (Collider2D hit in colliders){
            if (hit.GetComponent<Enemy>() != null){
                if (hit.GetComponent<Enemy>().CanBeStunned()){
                    stateTimer = 10;
                    player.anim.SetBool("CounterSuccessful", true);
                }
            }
        }
    }
}
