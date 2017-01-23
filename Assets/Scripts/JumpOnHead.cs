using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnHead : MonoBehaviour {

    public Vector3 bounceDirection;
    public float bounceSpeed;

    GameObject player;
    Rigidbody2D playerRig;

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        playerRig = player.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerRig.AddForce(bounceDirection * bounceSpeed);
        }
    }
}
