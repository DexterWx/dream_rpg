using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;

    protected float xInput;
    protected float stateTimer = 0;
    protected bool triggerCalled;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter(){
        player.anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Exit(){
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void Update(){
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        player.anim.SetFloat("YVelocity", player.rb.linearVelocity.y);
    }

    public virtual void AnimationTriggerFinish(){
        triggerCalled = true;
    }

}