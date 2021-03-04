using Assets.Scripts.CombatSystem;
using Assets.Scripts.Player_States;
using Assets.Scripts.States;
using Assets.Scripts.States.PlayerStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IAttacker, IAttackable {

    public Transform axeAttack;
    public float attackRadius = 0.5f;
	public LayerMask enemyMask;

    private Animator anim;
    public Animator Animator { get { return anim; } }


	[Header("Locomotive")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
	public float acceleration = 10f;

    public PlayerStateMachine StateMachine;
	[HideInInspector]
    public CharacterController2D CharacterController;
	[HideInInspector]
    public PlayerHealth health;
	[HideInInspector]
	public SpriteRenderer playerRenderer;

	private void Awake()
	{
        StateMachine = new PlayerStateMachine(this);
		playerRenderer = GetComponentInChildren<SpriteRenderer>();
	}

    void Start () {
		anim = GetComponentInChildren<Animator>();
        health = GetComponent<PlayerHealth>();
		CharacterController = GetComponent<CharacterController2D>();
	}

	public void OnTakeDamage(GameObject attacker, IAttackEffect[] effects)
	{
		StateMachine.currentState.OnTakeDamage(this, attacker, effects);
	}

	private void FixedUpdate()
	{
        StateMachine.currentState.FixedUpdate(this);
	}
	// Update is called once per frame
	void Update() {
        StateMachine.currentState.Update(this);
        //TODO: Refactor, test with fixed update ??
        var rb = CharacterController.m_Rigidbody2D;
		if (rb.velocity.y < 0)
		{
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		} else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
		{
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
	}

    //TODO: Refactor - it is too similar to enemy attack
    public void Attack()
	{
        var collders = Physics2D.OverlapCircleAll(axeAttack.position, attackRadius, enemyMask);
		for (int i = 0; i < collders.Length; i++)
		{
            var enemy = collders[i];
            var attackDelta = enemy.transform.position - this.transform.position; //could cache transform for micro optimization
            enemy.GetComponent<Health>().TakeDamage(attackDelta);
		}
	}
	private void OnDrawGizmosSelected()
	{
        Gizmos.DrawSphere(axeAttack.position, attackRadius);
	}

	public void PowerFullAttack()
	{
        var collders = Physics2D.OverlapCircleAll(axeAttack.position, attackRadius, enemyMask);
		for (int i = 0; i < collders.Length; i++)
		{
            var enemy = collders[i];
            var attackDelta = enemy.transform.position - this.transform.position; //could cache transform for micro optimization
            enemy.GetComponent<Health>().TakeCriticalDamage(attackDelta);
            var ai=enemy.GetComponent<AI>();
            ai.StateMachine.TransitionState(ai.StateMachine.stunnedState);
		}
	}
}
