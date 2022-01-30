﻿using Assets.Scripts.CombatSystem;
using Assets.Scripts.GameInput;
using Assets.Scripts.States;
using Assets.Scripts.States.PlayerStates;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayerController : MonoBehaviour, IAttacker, IAttackable {


    private Animator anim;
    public Animator Animator { get { return anim; } }


	[Header("Locomotive")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
	public float acceleration = 10f;

	[Header("Stamina")]
	[Range(0.001f, 1f)]
	public float staminaIncreasePerSecond = 1;
	public float attackStaminaCost = 0.2f;
	public float blockStaminaCost = 0.2f;
	[HideInInspector]
	public float staminaMultiplier = 1;
	public float staminaBaseMultiplier = 1;
	[Range(0.001f, 1f)]
	public float staminaMovingMultiplier = 0.75f;
	[Range(0.001f, 1f)]
	public float staminaFightingMultiplier = 0.5f;
	[Range(0, 1f)]
	public float staminaBlockingMultiplier = 0f;

	public PlayerStateMachine StateMachine;
	[HideInInspector]
    public CharacterController2D CharacterController;
	[HideInInspector]
    public Health health;
	[HideInInspector]
	public SpriteRenderer playerRenderer;
	[HideInInspector]
	public Attack attackScript;
	[HideInInspector]
	public SpriteFlash flash;
	[HideInInspector]
	public StaminaScript staminaScript;//TODO

	[HideInInspector]
    public InputState inputState; //TODO: can we refactor so that states inject this?

	[Inject]
	public void Construct(InputState inputState)
    {
		this.inputState = inputState;
    }

	private void Awake()
	{
        StateMachine = new PlayerStateMachine(this);
		attackScript = GetComponent<Attack>();
		playerRenderer = GetComponentInChildren<SpriteRenderer>();
		flash = GetComponent<SpriteFlash>();
	}

    void Start () {
		anim = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
		CharacterController = GetComponent<CharacterController2D>();
        health.OnDeath += OnDeath;

        staminaScript = GameObject.FindGameObjectWithTag("UI").GetComponent<StaminaScript>();
        staminaScript.OnExhausted += () => StateMachine.TransitionState(PlayerBaseState.exhaustedState);

        attackScript.OnAttack += () => staminaScript.ReduceStamina(attackStaminaCost);
	}

    public void OnTakeDamage(GameObject attacker, IAttackEffect[] effects)
	{
		StateMachine.currentState.OnTakeDamage(this, attacker, effects);
	}

	private void FixedUpdate()
	{
        StateMachine.currentState.FixedUpdate(this);
	}
	public void OnDeath()
    {
		SceneManager.LoadScene("Game Over");
    }
	// Update is called once per frame
	void Update() {
        StateMachine.currentState.Update(this);
        //TODO: Refactor, test with fixed update ??
        var rb = CharacterController.m_Rigidbody2D;
		if (rb.velocity.y < 0)
		{
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		} else if(rb.velocity.y > 0 && !inputState.IsHoldingJump)
		{
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
	}

	private IAttackEffect[] regularAttackEffects = new IAttackEffect[]
	{
		new PlayerRegularAttack()
	};
	private IAttackEffect[] heavyAttackEffects = new IAttackEffect[]
	{
		new PlayerHeavyAttack()
	};

    public void Attack()
	{
		this.attackScript.DoAttack(regularAttackEffects);
	}

	public void PowerFullAttack()
	{
		this.attackScript.DoAttack(heavyAttackEffects);
	}
}
