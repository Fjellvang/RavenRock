using Assets.Scripts.Player_States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float acceleration = 10f;

    public GameObject axeAttack;
    Lady dame;
    public Lady Dame => dame; //TODO: refacotr

    Animator anim;
    public Animator Animator { get { return anim; } }
    public bool Blocking;
    AudioSource kissSound;
    public AudioSource KissSound => kissSound;

    public CharacterController2D CharacterController;

    PlayerBaseState currentState = new PlayerIdleState();

    public readonly PlayerIdleState idleState = new PlayerIdleState();
    public readonly PlayerMovingState movingState = new PlayerMovingState();
    public readonly PlayerJumpingState jumpingState = new PlayerJumpingState();
    public readonly PlayerCarryingState playerCarryingState = new PlayerCarryingState();


    public void TransitionState(PlayerBaseState newState)
	{
        currentState.OnExitState(this);
        Debug.Log($"Changing state to {newState}");
        currentState = newState;
        currentState.OnEnterState(this);
	}


    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
        dame = FindObjectOfType<Lady>();
        kissSound = GetComponent<AudioSource>();
	}


	private void Awake()
	{
        TransitionState(idleState);
        CharacterController.OnLandEvent.AddListener((x) => {
            Debug.Log($"Was Grounded: {x}");
            TransitionState(idleState);
            });
	}
	// Update is called once per frame
	void Update() {
        currentState.Update(this);

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton7)) && !anim.GetBool("blockButton")){
			//Debug.Log("PRESS R2");
			//anim.SetBool("shootButton", true);
			anim.Play ("Attack");
           
            axeAttack.SetActive(true);
		} else{
            axeAttack.SetActive(false);
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
