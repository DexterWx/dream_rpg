using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Player : MonoBehaviour
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
    #endregion

    #region Components
    public Animator anim {get; private set;}
    public Rigidbody2D rb {get; private set;}
    #endregion

    [Header("Movement")]
    public float normMoveSpeed = 10.0f;
    public float jumpForce = 10.0f;

    [Header("Collision")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Dash")]
    public float dashSpeed = 20.0f;
    public float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1.5f;
    [SerializeField] private float dashCooldownTimer;


    public bool facingRight {get; private set;} = true;

    private void Awake(){
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
    }

    private void Start(){
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

    private void Update(){
        stateMachine.currentState.Update();
        CheckDashInput();
    }

    public IEnumerator BusyFor(float _seconds){
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void SetVelocity(float _xVelocity, float _yVelocity){
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        ControlFlip(_xVelocity);
    }

    public bool CheckIfGrounded(){
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    public bool CheckIfTouchingWall(){
        return Physics2D.Raycast(wallCheck.position, Vector2.right * (facingRight ? 1 : -1), wallCheckDistance, whatIsGround);
    }

    public void OnDrawGizmos(){
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * (facingRight ? 1 : -1) * wallCheckDistance);
    }

    public void Flip(){
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void ControlFlip(float _xVelocity){
        if (_xVelocity * (facingRight ? 1 : -1) < 0)
            Flip();
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
