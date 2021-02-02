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
    public GameObject monkHead;

	private void Awake()
	{
		audioPlayer = GetComponent<AudioSource>();
	}
	internal void TakeDamage(int v)
	{
		audioPlayer.PlayOneShot(hitSound);
		Debug.Log("taking dmg!");
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