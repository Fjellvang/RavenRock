using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
	private Animator animator;
	private Rigidbody2D rig;
	private void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		rig = GetComponent<Rigidbody2D>();
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		OnCollision();
	}

	public void OnCollision()
	{
		animator.SetTrigger("Collided");
		rig.velocity = Vector2.zero;
		GetComponent<CircleCollider2D>().enabled = false;
		Invoke(nameof(DestroySelf), .5f);
		//animator.GetCurrentAnimatorStateInfo(0).
	}
	public void DestroySelf()
	{
		Destroy(gameObject);
	}
}
