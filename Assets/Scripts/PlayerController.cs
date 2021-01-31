using Assets.Scripts.Player_States;
using Assets.Scripts.States.PlayerStates;
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

    public readonly Stack<PlayerLocomotiveBaseState> stateStack = new Stack<PlayerLocomotiveBaseState>();
    PlayerLocomotiveBaseState currentLocomotiveState;
    PlayerLocomotiveBaseState currentEquipmentState;

    public void PoplastState()//TODO: Get better naming.
	{
        currentLocomotiveState.OnExitState(this);
        currentLocomotiveState = stateStack.Pop();//TODO: nullcheck ?
        Debug.Log($"popped to: {currentLocomotiveState}");
        currentLocomotiveState.OnEnterState(this);
	}
    public void TransitionState(PlayerLocomotiveBaseState newState)
	{
        currentLocomotiveState.OnExitState(this);
        stateStack.Push(currentLocomotiveState);
        currentLocomotiveState = newState;
        Debug.Log($"transitioned to: {currentLocomotiveState}");
        currentLocomotiveState.OnEnterState(this);
	}


    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
        dame = FindObjectOfType<Lady>();
        kissSound = GetComponent<AudioSource>();
	}


	private void Awake()
	{
        currentLocomotiveState = PlayerLocomotiveBaseState.idleState;
	}
	private void FixedUpdate()
	{
        currentLocomotiveState.FixedUpdate(this);
	}
	// Update is called once per frame
	void Update() {
        currentLocomotiveState.Update(this);

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
