using UnityEngine;

public class EnemyAnimTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Enemy enemy => GetComponentInParent<Enemy>();

    public void AnimationTrigger(){
        enemy.AnimationTrigger();
    }

    public void TriggerAttack(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackRadius);
        foreach (Collider2D hit in colliders){
            if (hit.GetComponent<Player>() != null){
                hit.GetComponent<Player>().Demage();
            }
        }
    }

    public void OpenCounterWindow(){
        enemy.OpenCounterWindow();
    }

    public void CloseCounterWindow(){
        enemy.CloseCounterWindow();
    }
}
