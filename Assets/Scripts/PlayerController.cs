using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float acceleration = 10f;

    public GameObject axeAttack;
    Lady dame;

    Animator anim;
    public bool Blocking;
    public bool carrying;
    AudioSource audio;

    public CharacterController2D CharacterController;


    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
        dame = FindObjectOfType<Lady>();
        carrying = anim.GetBool("isCarrying");
        audio = GetComponent<AudioSource>();
	}


	private void Awake()
	{
        CharacterController.OnCrouchEvent.AddListener((crouch) => Debug.Log("CROUCH"));
	}
	// Update is called once per frame
	void Update() {
        var axis = Input.GetAxis("Horizontal");


        //if (Mathf.Abs(m_Velocity.x) <= 0.01f) {
        //    anim.SetBool("isMoving", true);
        //}
        //else
        //{
        //    anim.SetBool("isMoving", false);
        //}
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton7)) && !anim.GetBool("blockButton")){
			//Debug.Log("PRESS R2");
			//anim.SetBool("shootButton", true);
			anim.Play ("Attack");
           
            axeAttack.SetActive(true);
		} else{
            axeAttack.SetActive(false);
		}
        var jump = Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Space);


        CharacterController.Move(axis * acceleration * Time.deltaTime, false, jump);

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
