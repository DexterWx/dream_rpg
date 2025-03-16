using UnityEngine;

public class PlayerAnimatorTrigger : MonoBehaviour
{
    public Player player => GetComponentInParent<Player>();

    public void AnimationTrigger(){
        player.AnimationTrigger();
    }

    public void TriggerAttack(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackRadius);
        foreach (Collider2D hit in colliders){
            if (hit.GetComponent<Enemy>() != null){
                hit.GetComponent<Enemy>().Demage();
            }
        }
    }
}
