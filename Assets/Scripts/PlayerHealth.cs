using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float health = 5;
    public AudioClip hitSound;
    public AudioClip blockedAttackSound;
    AudioSource audioPlayer;

    //Events
    //TODO: Shoudl this be here?
    public delegate void HitAction();
    public static event HitAction OnHit;

    private void Awake()
	{
        audioPlayer = GetComponent<AudioSource>();
	}

	
    public void TakeDmg()
    {
		OnHit?.Invoke();
        //TODO: Make audio manager.
		audioPlayer.PlayOneShot(hitSound);
        health--;
        
        if (health < 0)
        {
            //we dead
            Destroy(gameObject);
            Application.LoadLevel("Game Over");
        }
    }

	internal void Block()
	{
		audioPlayer.PlayOneShot(blockedAttackSound);
	}
}
