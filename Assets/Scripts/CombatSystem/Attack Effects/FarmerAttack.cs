using Assets.Scripts.CombatSystem;
using UnityEngine;

public class FarmerAttack : MonoBehaviour, IAttackEffect
{
    private FarmerController ai;
    public AudioClip blockedSound;
    public AudioSource audioSource;
    public void OnFailedAttack(GameObject attacker, GameObject attacked)
    {
        audioSource.PlayOneShot(blockedSound);
        ai.stateMachine.TransitionState(ai.stateMachine.stunnedState);
    }

    public void OnSuccessFullAttack(GameObject attacker, GameObject attacked)
    {
        attacked.GetComponent<Health>().TakeDamage(attacker, 1);
    }

    // Start is called before the first frame update
    void Awake() { 
        ai = GetComponent<FarmerController>();
    }
    
}
