﻿using Assets.Scripts.CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public bool withinRange= false;

    public Transform axeAttack;
    public float attackRadius = 0.5f;
    public LayerMask enemyMask;
    private Transform thisTransform;
	private void Awake()
	{
        this.thisTransform = transform;
	}


	public void DoAttack(IAttackEffect[] attackEffects)
    {
        var collders = Physics2D.OverlapCircleAll(axeAttack.position, attackRadius, enemyMask);
        for (int i = 0; i < collders.Length; i++)
        {
            var enemy = collders[i];
            enemy.GetComponent<PlayerController>().OnTakeDamge(this.gameObject, attackEffects);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(axeAttack.position, attackRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            withinRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            withinRange = false;
        }
    }

}
