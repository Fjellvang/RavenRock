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

    public readonly Stack<PlayerBaseState> stateStack = new Stack<PlayerBaseState>();
    PlayerBaseState currentState;

    public void PoplastState()//TODO: Get better naming.
	{
        currentState.OnExitState(this);
        currentState = stateStack.Pop();//TODO: nullcheck ?
        Debug.Log($"popped to: {currentState}");
        currentState.OnEnterState(this);
	}
    public void TransitionState(PlayerBaseState newState)
	{
        currentState.OnExitState(this);
        stateStack.Push(currentState);
        currentState = newState;
        Debug.Log($"transitioned to: {currentState}");
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
        currentState = PlayerBaseState.idleState;
	}
	private void FixedUpdate()
	{
        currentState.FixedUpdate(this);
	}
	// Update is called once per frame
	void Update() {
        currentState.Update(this);

  //      if (Input.GetButtonDown("Attack")) 
  //      { 
		//	//Debug.Log("PRESS R2");
		//	//anim.SetBool("shootButton", true);
		//	anim.Play ("Attack");
           
  //          axeAttack.SetActive(true);
		//} else{
  //          axeAttack.SetActive(false);
		//}


  //      if (Input.GetButton("Block"))
  //      {
  //          // Circle
  //          anim.SetBool("blockButton", true);
  //          Blocking = true;
  //          anim.Play("Blocking");
  //      }
  //      else
  //      {
  //          Blocking = false;
  //          anim.SetBool("blockButton", false);
  //      }
       
	}
}
