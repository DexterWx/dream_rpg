using UnityEngine;

public class Enemy : Entity
{

    public EnemyStateMachine stateMachine {get; private set;}

    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected float playerCheckDistance;
    

    [Header("Attack")]
    public float attackDistance;
    public float attackColdDown;
    public float battleTime;
    [HideInInspector] public float lastAttackTime;


    [Header("Stun")]
    public float stunDuration;
    public Vector2 stunDirection;
    public bool canBeStunned {get; private set;} = false;
    [SerializeField] private GameObject counterImage;





    protected override void Awake(){
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Start(){
        base.Start();
    }


    protected override void Update(){
        base.Update();
        stateMachine.currentState.Update();
    }


    public virtual RaycastHit2D isPlayerDetected(){
        return Physics2D.Raycast(transform.position, Vector2.right * (facingRight ? 1 : -1), playerCheckDistance, whatIsPlayer);
    }

    // public override void OnDrawGizmos(){
    //     base.OnDrawGizmos();
    //     // 设置检测范围线的颜色（比如红色）
    //     // Gizmos.color = Color.red;
    //     // Gizmos.DrawLine(transform.position, transform.position + Vector3.right * (facingRight ? 1 : -1) * playerCheckDistance);
        
    //     // 设置攻击范围线的颜色（比如黄色）
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawLine(transform.position, transform.position + Vector3.right * (facingRight ? 1 : -1) * attackDistance);
    // }


    public void AnimationTrigger(){
        stateMachine.currentState.AnimationTriggerFinish();
    }

    public virtual void OpenCounterWindow(){
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterWindow(){
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned(){
        if (canBeStunned){
            CloseCounterWindow();
            return true;
        }
        else{
            return false;
        }
    }

}

