using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 3;
	public AudioClip hitSound;
	public AudioSource audioPlayer;
    public GameObject monkBody;
	private Rigidbody2D rig;
    public GameObject monkHead;

	private void Awake()
	{
		audioPlayer = GetComponent<AudioSource>();
		rig = GetComponent<Rigidbody2D>();
	}
	internal void TakeDamage(Vector2 delta)
	{
		audioPlayer.PlayOneShot(hitSound);
		rig.AddForce(delta * 3, ForceMode2D.Impulse);
        health--;
		if (health < 0)
		{
			Destroy(this.gameObject);
			if(monkBody)
				Instantiate(monkBody, transform.position, transform.rotation);
			if(monkHead)
				Instantiate(monkHead, transform.position += new Vector3(0, 1f, 0f), transform.rotation);
		}
	}
}