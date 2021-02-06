using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float health = 5;
    HealthBar hb;
    CustomImageEffect cameraEffect;
    public AudioClip hitSound;
    public AudioClip blockedAttackSound;
    AudioSource audioPlayer;

    private void Awake()
	{
        cameraEffect = Camera.main.GetComponent<CustomImageEffect>();
        audioPlayer = GetComponent<AudioSource>();
		if (cameraEffect == null)
		{
            Debug.LogError("NO EFFECT FOUND ON CAMERA");
		}
	}

	// Use this for initialization
	void Start () {
        hb = FindObjectOfType<HealthBar>();
        int pic = (int)health;
        hb.SetPicture(pic);
	}
	
    public void TakeDmg()
    {
        cameraEffect.DoEffect();
        audioPlayer.PlayOneShot(hitSound);
        health--;
        
        if (health < 0)
        {
            //we dead
            Destroy(gameObject);
            Application.LoadLevel("Game Over");
        }
        if(health < 6 && health > 0) { 
            hb.SetPicture(((int)health)-1);
        }
    }

	internal void Block()
	{
        Debug.Log("block");
		audioPlayer.PlayOneShot(blockedAttackSound);
	}
}
