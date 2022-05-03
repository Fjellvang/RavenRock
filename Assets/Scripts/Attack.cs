using Assets.Scripts.CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public Transform axeAttack;
    public float attackRadius = 0.5f;
    public LayerMask enemyMask;
    public float attackGracePeriod = 1f; //How long is a successfull attack valid
    public float timeSinceSuccessfullAttack = 0f;
    private void Awake()
    {
        
    }
    [HideInInspector]
    public bool successfullyAttacked;

    public delegate void OnAttackAction();
    public event OnAttackAction OnAttack;

    public delegate void OnAttackHitAction();
    public event OnAttackHitAction OnAttackHit;

    public void DoAttack(IAttackEffect[] attackEffects)
    {
        var collders = Physics2D.OverlapCircleAll(axeAttack.position, attackRadius, enemyMask);
        OnAttack?.Invoke();
        for (int i = 0; i < collders.Length; i++)
        {
            var enemy = collders[i];
            enemy.GetComponent<IAttackable>().OnTakeDamage(this.gameObject, attackEffects);
            OnAttackHit?.Invoke();
            successfullyAttacked = true;
            timeSinceSuccessfullAttack = attackGracePeriod;
        }
    }
    private void Update()
    {
        timeSinceSuccessfullAttack -= Time.deltaTime;
        if (timeSinceSuccessfullAttack <= 0)
        {
            successfullyAttacked = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(axeAttack.position, attackRadius);
    }

    public bool WithinRange(Transform transform)
    {
        var weaponToPlayer = axeAttack.position - transform.position;
        var withinRange = Mathf.Abs(weaponToPlayer.x) <= attackRadius;
        return withinRange;
    }
}
