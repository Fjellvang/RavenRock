using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	public float acceleration = 10f;
    public float jumpspeed = 10;
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
	private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    private bool m_FacingRight;

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
        rig = GetComponent<Rigidbody2D>();
        dame = FindObjectOfType<Lady>();
        carrying = anim.GetBool("isCarrying");
        audio = GetComponent<AudioSource>();
	}


    private static readonly Vector3 right = new Vector3(1, 1, 1);
    private static readonly Vector3 left = new Vector3(-1, 1, 1);

    // Update is called once per frame
    void Update() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatisGround);
        if (grounded) { tripleJump = 1; }
        axis = Input.GetAxis("Horizontal");

        Move(axis * acceleration * Time.deltaTime, grounded, false);

        if (Mathf.Abs(m_Velocity.x) <= 0.01f) {
            anim.SetBool("isMoving", true);
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

    public void Move(float move,bool grounded, bool jump)
    {

        //only control the player if grounded or airControl is turned on
        if (grounded)
        {

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, rig.velocity.y);
            // And then smoothing it out and applying it to the character
            rig.velocity = Vector3.SmoothDamp(rig.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                m_FacingRight = true;
				transform.localScale = right;
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                m_FacingRight = false;
				transform.localScale = left;
            }
        }
        // If the player should jump...
        //if (m_Grounded && jump)
        //{
        //    // Add a vertical force to the player.
        //    m_Grounded = false;
        //    rig.AddForce(new Vector2(0f, m_JumpForce));
        //}
    }


}
