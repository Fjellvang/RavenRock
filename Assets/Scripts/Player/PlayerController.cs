using Assets.Scripts.CombatSystem;
using Assets.Scripts.Game;
using Assets.Scripts.GameInput;
using Assets.Scripts.Player;
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
    private GameState gameState;
    [HideInInspector]
    public InputState inputState; //TODO: can we refactor so that states inject this?
    public PlayerStaminaManager playerStaminaManager;
	[HideInInspector]
    public PlayerSettings playerSettings;
	[HideInInspector]
	public PlayerCombatManager combatManager;

    [Inject]
	public void Construct(InputState inputState, PlayerStaminaManager playerStaminaManager, PlayerSettings playerSettings, GameState gameState, PlayerCombatManager combatManager)
    {
		this.gameState = gameState; 
		this.inputState = inputState;
		this.playerStaminaManager = playerStaminaManager; 
		this.playerSettings = playerSettings; //Maybe we should NOT control this from the controller...
		this.combatManager = combatManager;
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

		playerSettings.StaminaMultiplier = staminaBaseMultiplier;
		playerSettings.StaminaIncreasePerSecond = staminaIncreasePerSecond;
		playerSettings.SuccessfullAttacksBeforeSuperAttack = 3; //TODO: Make editable.

		playerStaminaManager.OnExhausted += () => StateMachine.TransitionState(PlayerBaseState.exhaustedState);

        attackScript.OnAttack += () => playerStaminaManager.ReduceStamina(attackStaminaCost);
		attackScript.OnAttackHit += () => combatManager.OnAttackHit();
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
        if (gameState.IsPaused)
        {
			return;
        }
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
