using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour{

    public float moveSpeed = 10f;
    public float velocityLim = 10f;

    GameObject player;
    Vector3 delta;
    Rigidbody2D rig;
    bool facingRight;
    Visible playerVision;
    Attack atck;

    public float Ai;

    

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        rig = GetComponent<Rigidbody2D>();
        playerVision = FindObjectOfType<Visible>();
        atck = FindObjectOfType<Attack>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (atck)
        {
            if (playerVision.visibleList.Contains(gameObject) && !atck.withinRange)
            {
                // Only move if the enemy is within range of the player
                delta = player.transform.position - transform.position;
                delta.Normalize();
                //we dont want them to fly
                delta.y = 0;
            }
        }
        else
        {
            if(playerVision.visibleList.Contains(gameObject) && !atck.withinRange)
            {
                // Only move if the enemy is within range of the player
                delta = player.transform.position - transform.position;
                delta.Normalize();
                //we dont want them to fly
                delta.y = 0;
            }
        }
        if(rig.velocity.x < 0 && !facingRight)
        {
            facingRight = true;
            Flip();
        } else if(rig.velocity.x > 0 && facingRight)
        {
            facingRight = false;
            Flip();
        }
        

        if (rig.velocity.x < velocityLim)
        {
            if (gameObject.tag == "Enemy")
            {
                rig.AddForce(delta * moveSpeed);
            }
            else
            {
                rig.AddForce(delta * -moveSpeed);
            }
        }
        
	}

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public void StopMoving()
    {
        rig.velocity *= 0;
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
