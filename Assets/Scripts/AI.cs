using Assets.Scripts.Player_States;
using Assets.Scripts.States.EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class AI : MonoBehaviour{

    public float moveSpeed = 10f;

    Vector3 vectorTowardsPlayer;
    //public float Ai;

    public bool witinRange = false;

    [HideInInspector]
    public CharacterController2D controller;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public Attack weapon;
    [HideInInspector]
    public Animator Animator;

    EnemyBaseState currentState;


    //States
    public readonly AttackingState attackingState = new AttackingState();
    public readonly MovingState movingState = new MovingState();

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        weapon = GetComponentInChildren<Attack>();
        Animator = GetComponent<Animator>();

        currentState = movingState;
        controller = GetComponent<CharacterController2D>();
    }
	

    public void TransitionState(EnemyBaseState newState)
	{
        currentState.OnExitState(this);
        currentState = newState;
        currentState.OnEnterState(this);
	}

	private void FixedUpdate()
	{
        currentState.FixedUpdate(this);
	}

    public void Attack()
	{
        Debug.Log("Enemy Attacking!");
        //TODO: this was required to access the method in animator. Investigate why.
        this.weapon.DoAttack();
	}

	// Update is called once per frame
	void Update () {
        currentState.Update(this);
	}

}
