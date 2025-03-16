using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Enemy_Skeleton skeleton;
    private Transform player;
    public SkeletonBattleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
        skeleton = _enemy as Enemy_Skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
    }



    public override void Exit() 
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player.position.x > skeleton.transform.position.x) {
            if (!skeleton.CheckIfGrounded()){
                stateMachine.ChangeState(skeleton.idleState);
            }
            else{
                skeleton.SetVelocity(skeleton.moveSpeed, skeleton.rb.linearVelocity.y);
            }
        }
        else{
            if (!skeleton.CheckIfGrounded()){
                stateMachine.ChangeState(skeleton.idleState);
            }
            else{
                skeleton.SetVelocity(-skeleton.moveSpeed, skeleton.rb.linearVelocity.y);
            }
        }
        if (skeleton.isPlayerDetected()){
            stateTimer = skeleton.battleTime;
            if (Vector2.Distance(player.position, skeleton.transform.position) < skeleton.attackDistance) {
                if (canAttack()){
                    stateMachine.ChangeState(skeleton.attackState);
                }
            }
        }

        else {
            if (stateTimer < 0){
                stateMachine.ChangeState(skeleton.idleState);
            }
        }




    }

    public bool canAttack(){
        if (Time.time >= skeleton.lastAttackTime + skeleton.attackColdDown){
            return true;
        }
        return false;
    }
}

