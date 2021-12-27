using Assets.Scripts;
using Assets.Scripts.Android_UI;
using Assets.Scripts.Player_States;
using Assets.Scripts.States.PlayerStates;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.CombatSystem;
using Assets.Scripts.States;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IAttacker, IAttackable {


    private Animator anim;
    public Animator Animator { get { return anim; } }

    public IInputHandler InputHandler;
    public Joystick joystick;

	[Header("Locomotive")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
	public float acceleration = 10f;

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

	private void Awake()
	{
        StateMachine = new PlayerStateMachine(this);
		attackScript = GetComponent<Attack>();
		playerRenderer = GetComponentInChildren<SpriteRenderer>();
		flash = GetComponent<SpriteFlash>();
		InputHandler = AndroidButtonHandler.Instance;
		joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
	}

	void Start () {
		anim = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
		CharacterController = GetComponent<CharacterController2D>();
        health.OnDeath += OnDeath;
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
		} else if(rb.velocity.y > 0 && !AndroidButtonHandler.Instance.JumpButton)
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
