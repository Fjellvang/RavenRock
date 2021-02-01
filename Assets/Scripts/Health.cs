using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 3;
    public GameObject monkBody;
    public GameObject monkHead;

	internal void TakeDamage(int v)
	{
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