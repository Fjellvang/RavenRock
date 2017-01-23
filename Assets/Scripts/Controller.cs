using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	public float friction = 0.5f;
	public float acceleration = 10f;
    public float jumpspeed = 10;
	float velocity = 0;
    Physics2D ray;
    bool grounded = false;
    float axis = 0;


    public GameObject axeAttack;
    Rigidbody2D rig;
    Lady dame;

    
    public LayerMask whatisGround;
    Animator anim;
    public Transform groundCheck;
    public float groundRadius;
    public int tripleJump = 1;
    public bool Blocking;
    public bool carrying;
    AudioSource audio;
    
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
        rig = GetComponent<Rigidbody2D>();
        dame = FindObjectOfType<Lady>();
        carrying = anim.GetBool("isCarrying");
        audio = GetComponent<AudioSource>();
	}



    // Update is called once per frame
    void Update() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatisGround);
        if (grounded) { tripleJump = 1; }
        axis = Input.GetAxis("Horizontal");


        Vector3 pos = transform.position;


        if (axis != 0 && !anim.GetBool("blockButton")) {
            velocity += acceleration * Time.deltaTime * axis;
        }
        if (velocity != 0) {
            velocity -= velocity * friction * Time.deltaTime;
            this.transform.position += new Vector3(velocity, 0, 0);
            anim.SetBool("isMoving", true);
        }

        if (axis < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (axis > 0) {
            transform.localScale = new Vector3(1, 1, 1);

        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton7)) && !anim.GetBool("blockButton")){
			//Debug.Log("PRESS R2");
			//anim.SetBool("shootButton", true);
			anim.Play ("Attack");
           
            axeAttack.SetActive(true);
		} else{
            axeAttack.SetActive(false);
		}
        if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Space))
        {
            //X
            if (tripleJump > 0 || grounded)
            {
                tripleJump--;
                rig.AddForce(Vector2.up * jumpspeed);
            }
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.E))
        {
            carrying = anim.GetBool("isCarrying");

            if (dame.isTouching && !anim.GetBool("isCarrying"))
            {
                carrying = true;
                audio.Play();
                anim.SetBool("isCarrying", true);
                dame.DestroyThis();
            }
            else if (anim.GetBool("isCarrying"))
            {
                anim.SetBool("isCarrying", false);
                dame.SpawnNew(this.transform);
            }


        }

        if (Input.GetKey(KeyCode.JoystickButton2))
        {
            // Circle
            anim.SetBool("blockButton", true);
            Blocking = true;
            anim.Play("Blocking");
        }
        else
        {
            Blocking = false;
            anim.SetBool("blockButton", false);
        }
       
	}

    
}
