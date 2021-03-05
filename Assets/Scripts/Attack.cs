using Assets.Scripts.CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public Transform axeAttack;
    public float attackRadius = 0.5f;
    public LayerMask enemyMask;

	public void DoAttack(IAttackEffect[] attackEffects)
    {
        var collders = Physics2D.OverlapCircleAll(axeAttack.position, attackRadius, enemyMask);
        for (int i = 0; i < collders.Length; i++)
        {
            var enemy = collders[i];
            enemy.GetComponent<IAttackable>().OnTakeDamage(this.gameObject, attackEffects);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(axeAttack.position, attackRadius);
    }
}
