using Assets.Scripts.Player_States;
using Assets.Scripts.States.EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController : MonoBehaviour { }

[RequireComponent(typeof(CharacterController2D))]
public class AI : EntityController, IAttack{

    public float moveSpeed = 10f;
    public float stunnedDuration = 2f;

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

    [HideInInspector]
    public SpriteRenderer SpriteRenderer;

    EnemyBaseState currentState;



    //States
    public readonly AttackingState attackingState = new AttackingState();
    public readonly MovingState movingState = new MovingState();
    public readonly EnemyStunnedState stunnedState = new EnemyStunnedState();

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        weapon = GetComponentInChildren<Attack>();
        Animator = GetComponentInChildren<Animator>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

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
        //TODO: this was required to access the method in animator. Investigate why.
        var successfull = this.weapon.DoAttack();
		if (!successfull)
		{
            //we get stunned
            TransitionState(stunnedState);
		}
	}

	// Update is called once per frame
	void Update () {
        currentState.Update(this);
	}

}
