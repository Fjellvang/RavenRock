﻿using Assets.Scripts.CombatSystem.DamageEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteFlash))]
[RequireComponent(typeof(AttackedEffects))]
public class Health : MonoBehaviour
{
    public float health = 3;
	public AudioClip hitSound;
	public AudioSource audioPlayer;
	private SpriteFlash spriteFlash;
	private AttackedEffects attackedEffects;

	//TODO: Shoudl this be here?
	public delegate void DeathAction();
	public event DeathAction OnDeath;

	private float originalHealth = 0;

	private void Awake()
	{
		audioPlayer = GetComponent<AudioSource>();
		spriteFlash = GetComponent<SpriteFlash>();

		attackedEffects = GetComponent<AttackedEffects>();
		originalHealth = health;
	}

	public void ResetHealth()
	{
		health = originalHealth;
		spriteFlash.EnsureReset();
	}
	public void TakeDamage(GameObject attacker)
	{
		audioPlayer.PlayOneShot(hitSound);
		spriteFlash.Flash();
		attackedEffects.OnDamage(attacker);
		health--;
		EvaluateHealth();
	}

	public void TakeCriticalDamage(GameObject attacker)
	{
		audioPlayer.PlayOneShot(hitSound);
		attackedEffects.OnCriticalDamage(attacker);
		spriteFlash.Flash();
		health -= 2;
		EvaluateHealth();
	}

	private void EvaluateHealth()
	{
		if (health < 0)
		{
			OnDeath?.Invoke();
		}
	}
}