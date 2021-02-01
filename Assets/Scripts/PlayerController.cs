using Assets.Scripts.Player_States;
using Assets.Scripts.States.PlayerStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float acceleration = 10f;

    public Transform axeAttack;
    public float attackRadius = 0.5f;
	public LayerMask enemyMask;
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
        currentState.OnEnterState(this);
	}
    public void TransitionState(PlayerBaseState newState)
	{
        currentState.OnExitState(this);
        stateStack.Push(currentState);
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
        currentState = PlayerBaseState.idleState;
	}
	private void FixedUpdate()
	{
        currentState.FixedUpdate(this);
	}
	// Update is called once per frame
	void Update() {
        currentState.Update(this);
	}

    public void Attack()
	{
        var collders = Physics2D.OverlapCircleAll(axeAttack.position, attackRadius, enemyMask);
		for (int i = 0; i < collders.Length; i++)
		{
            var enemy = collders[i];
            enemy.GetComponent<Health>().TakeDamage(10);
		}
	}
	private void OnDrawGizmosSelected()
	{
        Gizmos.DrawSphere(axeAttack.position, attackRadius);
	}
}
