using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float health = 5;
    HealthBar hb;

    // Use this for initialization
    void Start () {
        hb = FindObjectOfType<HealthBar>();
        int pic = (int)health;
        hb.SetPicture(pic);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDmg()
    {
        Debug.Log(health);
        health--;
        
        if (health < 0)
        {
            //we dead
            Destroy(gameObject);
            Application.LoadLevel("dead");
        }
        if(health < 6 && health > 0) { 
            hb.SetPicture((int)health);
        }
    }
}
