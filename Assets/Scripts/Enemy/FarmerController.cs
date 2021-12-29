using Assets.Scripts.CombatSystem;
using Assets.Scripts.Enemy;
using Assets.Scripts.States;
using Assets.Scripts.States.EnemyStates.FarmerStates;
using System;
using UnityEngine;


public class FarmerController : NPCControllerBase<FarmerStateMachine, EnemyBaseState, FarmerController>,
    IAttacker, IAttackable, IStunnable {

    [HideInInspector]
    public float stunnedDuration = 2f;
    [HideInInspector]
    public bool playerVisible = false;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public Attack weapon;
    [HideInInspector]
    public Animator Animator;
    public IAttackEffect[] attacks;

    [HideInInspector]
    public SpriteRenderer SpriteRenderer;

    public override Func<FarmerStateMachine> ConstructStatemachine => () => new FarmerStateMachine(this);

    protected override void Awake()
	{
        base.Awake();
        GetComponent<Health>().OnDeath += OnDeath;
    }

    private void OnDeath()
	{
        stateMachine.TransitionState(stateMachine.deadState);
    }

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        weapon = GetComponentInChildren<Attack>();
        Animator = GetComponentInChildren<Animator>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        attacks = GetComponents<IAttackEffect>();

        controller = GetComponent<CharacterController2D>();
    }
	

    public void Attack()
	{
        weapon.DoAttack(attacks);
	}

	public void PowerFullAttack()
	{
		throw new System.NotImplementedException();
	}

	public override void OnTakeDamage(GameObject attacker, IAttackEffect[] attackEffects)
	{
		for (int i = 0; i < attackEffects.Length; i++)
		{
            attackEffects[i].OnSuccessFullAttack(attacker, gameObject);
		}
	}

	public void Stun(float duration)
	{
        this.stunnedDuration = duration;
        this.stateMachine.TransitionState(stateMachine.stunnedState);
	}
}
