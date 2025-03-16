using UnityEngine;

public class SkeletonGroudState : EnemyState
{
    private Enemy_Skeleton skeleton;
    protected Transform player;
    public SkeletonGroudState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
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
        if((skeleton.isPlayerDetected() || Vector2.Distance(skeleton.transform.position, player.position) <= skeleton.attackDistance) && skeleton.CheckIfGrounded()){
            stateMachine.ChangeState(skeleton.battleState);
        }
    }


}
