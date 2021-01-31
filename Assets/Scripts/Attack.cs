using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    GameObject player;
    PlayerController pController;
    PlayerHealth playerHealth;
    AI ai;
    public bool withinRange= false;

    public float secs = 0.5f, orig;
    

    // Use this for initialization
    void Start () {
        pController = FindObjectOfType<PlayerController>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        orig = secs;
        ai = FindObjectOfType<AI>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (withinRange)
        {
            secs -= Time.deltaTime;
            if (secs < 0)
            {
                playerHealth.TakeDmg();
                secs = orig;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            withinRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            withinRange = false;
        }
    }

}
