using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    GameObject player;
    Controller pController;
    Animator anim;
    PlayerHealth playerHealth;
    AI ai;
    public bool withinRange= false;

    public float secs = 0.5f, orig;
    

    // Use this for initialization
    void Start () {
        anim = GetComponentInParent<Animator>();
        pController = FindObjectOfType<Controller>();
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
            anim.SetBool("inRange", true);
            ai.StopMoving();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetBool("inRange", false);
            withinRange = false;
        }
    }

}
