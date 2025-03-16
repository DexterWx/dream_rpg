using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack Detail")]
    public Vector2[] attackMovement;

    public bool isBusy {get; private set;}
    #region State
    public PlayerStateMachine stateMachine {get; private set;}
    public PlayerIdleState idleState {get; private set;}
    public PlayerMoveState moveState {get; private set;}
    public PlayerJumpState jumpState {get; private set;}
    public PlayerAirState airState {get; private set;}
    public PlayerDashState dashState {get; private set;}
    public PlayerWallSlideState wallSlideState {get; private set;}
    public PlayerWallJumpState wallJumpState {get; private set;}
    public PlayerAttackState attackState {get; private set;}
    public PlayerCounterAttackState counterAttackState {get; private set;}
    #endregion



    [Header("Movement")]
    public float normMoveSpeed = 10.0f;
    public float jumpForce = 10.0f;

    [Header("Dash")]
    public float dashSpeed = 20.0f;
    public float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1.5f;
    [SerializeField] private float dashCooldownTimer;


    [Header("Counter")]
    public float counterDuration = 0.2f;


    protected override void Awake(){
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");

        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");

        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
    }


    protected override void Start(){
        base.Start();
        stateMachine.Initialize(idleState);
    }


    protected override void Update(){
        base.Update();
        stateMachine.currentState.Update();
        CheckDashInput();
    }




    public IEnumerator BusyFor(float _seconds){
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }


    public void CheckDashInput(){
        dashCooldownTimer -= Time.deltaTime;

        if (CheckIfTouchingWall()){
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0){
            stateMachine.ChangeState(dashState);
            dashCooldownTimer = dashCooldown;
        }
    }

    public void AnimationTrigger(){
        stateMachine.currentState.AnimationTriggerFinish();
    }

}
