using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [Header("Attack")]
    public Transform attackCheck;
    public float attackRadius;

    [Header("Attack Impact")]
    [SerializeField] private Vector2 impactDirection;
    [SerializeField] private float impactDuration;


    #region Components

    public Animator anim {get; private set;}
    public Rigidbody2D rb {get; private set;}
    public EntityFX entityFX {get; private set;}
    #endregion


    [Header("Collision")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;


    private bool isKnockedBack = false;



    public bool facingRight {get; private set;} = true;

    protected virtual void Awake(){
    }


    protected virtual void Start(){
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        entityFX = GetComponent<EntityFX>();
    }


    protected virtual void Update(){
    }


    public bool CheckIfGrounded(){
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    public bool CheckIfTouchingWall(){
        return Physics2D.Raycast(wallCheck.position, Vector2.right * (facingRight ? 1 : -1), wallCheckDistance, whatIsGround);
    }

    public virtual void OnDrawGizmos(){
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * (facingRight ? 1 : -1) * wallCheckDistance);
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
    }


    public void Flip(){
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void ControlFlip(float _xVelocity){
        if (_xVelocity * (facingRight ? 1 : -1) < 0)
            Flip();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity){
        if (isKnockedBack) return;
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        ControlFlip(_xVelocity);
    }


    public void Demage() {
        StartCoroutine(entityFX.FlashFX());
        StartCoroutine(Impact());
    }


    private IEnumerator Impact(){
        isKnockedBack = true;
        rb.linearVelocity = new Vector2(impactDirection.x * (facingRight ? -1 : 1), impactDirection.y);
        yield return new WaitForSeconds(impactDuration);
        isKnockedBack = false;

    }




}
