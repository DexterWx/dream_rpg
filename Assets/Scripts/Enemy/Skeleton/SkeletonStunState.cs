using UnityEngine;

public class SkeletonStunState : EnemyState
{
    private Enemy_Skeleton skeleton;
    public SkeletonStunState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName){
        skeleton = _enemy as Enemy_Skeleton;
    }


    public override void Enter(){
        base.Enter();

        skeleton.entityFX.InvokeRepeating("RedColorBlink", 0, 0.1f);
        stateTimer = skeleton.stunDuration;
        skeleton.rb.linearVelocity = new Vector2(skeleton.stunDirection.x * (skeleton.facingRight ? -1 : 1), skeleton.stunDirection.y);

    }





    public override void Exit(){
        base.Exit();
        skeleton.entityFX.Invoke("CancelRedColorBlink",0);
    }


    public override void Update() {
        base.Update();
        if (stateTimer <= 0){
            stateMachine.ChangeState(skeleton.idleState);
        }

    }


}
