using UnityEngine;

public class PlayerAnimatorTrigger : MonoBehaviour
{
    public Player player => GetComponentInParent<Player>();

    public void TriggerAttack(){
        player.AnimationTrigger();
    }
}
