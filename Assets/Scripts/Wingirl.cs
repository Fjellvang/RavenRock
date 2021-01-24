using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wingirl : MonoBehaviour {

    PlayerController controls;

	// Use this for initialization
	void Start () {
        controls = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            if (controls.carrying)
            {
                Application.LoadLevel("YouWon");
                Debug.Log("YOU WIN");
            }
        }
    }
}
