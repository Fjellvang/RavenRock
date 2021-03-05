using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteFlash))]
public class Health : MonoBehaviour
{
    public float health = 3;
	public AudioClip hitSound;
	public AudioSource audioPlayer;
	private Rigidbody2D rig;
	public SpriteFlash spriteFlash;

	//TODO: Shoudl this be here?
	public delegate void DeathAction();
	public event DeathAction OnDeath;


	private void Awake()
	{
		audioPlayer = GetComponent<AudioSource>();
		rig = GetComponent<Rigidbody2D>();
		spriteFlash = GetComponent<SpriteFlash>();
	}
	public void TakeDamage(Vector2 delta)
	{
		audioPlayer.PlayOneShot(hitSound);
		spriteFlash.Flash();
		health--;
		EvaluateHealth();
	}

	public void TakeCriticalDamage(Vector2 delta)
	{
		audioPlayer.PlayOneShot(hitSound);
		spriteFlash.Flash();
		rig.AddForce(Vector2.up * 8 + delta * 3, ForceMode2D.Impulse);
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