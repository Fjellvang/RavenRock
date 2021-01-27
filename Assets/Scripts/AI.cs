using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class AI : MonoBehaviour{

    public float moveSpeed = 10f;

    GameObject player;
    Vector3 vectorTowardsPlayer;
    bool facingRight;
    Visible playerVision;
    Attack atck;
    //public float Ai;

    public bool witinRange = false;

    CharacterController2D controller;

    

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        playerVision = FindObjectOfType<Visible>();
        atck = FindObjectOfType<Attack>();

        controller = GetComponent<CharacterController2D>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (atck)
        {
            if (witinRange && !atck.withinRange)
            {
                // Only move if the enemy is within range of the player
                vectorTowardsPlayer = player.transform.position - transform.position;
				Debug.DrawRay(transform.position, vectorTowardsPlayer, Color.blue);
            }
        }

		bool weAreAggressiveAI = gameObject.tag == "Enemy";
        var direction = vectorTowardsPlayer.x > 0 ? 1 : -1;
        var directionionalForce = direction * moveSpeed * Time.deltaTime;
        Debug.DrawRay(transform.position, new Vector3(direction, 0), Color.red);
		if (weAreAggressiveAI)
		{
            controller.Move(directionionalForce, false, false);
		}
		else
		{
            controller.Move(-directionionalForce, false, false);
		}
        
	}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player" && !pController.Blocking)
    //    {
    //        if (this.tag != "Monk")
    //        {
    //            playerHealth.TakeDmg();
    //        }
    //    }
    //}
}
