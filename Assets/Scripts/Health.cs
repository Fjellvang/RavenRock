using Assets.Scripts.CombatSystem.DamageEffects;
using UnityEngine;

[RequireComponent(typeof(AttackedEffects))]
[RequireComponent(typeof(AudioSource))]
public class Health : MonoBehaviour
{
    public float health = 3;
	public AudioClip hitSound;
	private AudioSource audioPlayer;
	private SpriteFlash spriteFlash;
	private AttackedEffects attackedEffects;

	//TODO: Shoudl this be here?
	public delegate void DeathAction();
	public event DeathAction OnDeath;
	public delegate void OnHitAction();
	public event OnHitAction OnHit;

	private float originalHealth = 0;

	private void Start()
	{
		audioPlayer = GetComponent<AudioSource>(); //TODO: Move this out of health into manage maybe?
		attackedEffects = GetComponent<AttackedEffects>();
		originalHealth = health;
	}

	public void ResetHealth()
	{
		health = originalHealth;
		spriteFlash?.EnsureReset();
	}
	public void TakeDamage(GameObject attacker, float damage)
	{
        audioPlayer.PlayOneShot(hitSound);
        if (OnHit != null)
        {
            OnHit();
        }
		attackedEffects.OnDamage(attacker, damage);
		health -= damage;
		EvaluateHealth();
	}

	public void TakeCriticalDamage(GameObject attacker, float damage)
	{
        audioPlayer.PlayOneShot(hitSound);
        if (OnHit != null)
        {
            OnHit();
        }
		attackedEffects.OnCriticalDamage(attacker, damage);
		health -= damage;
		EvaluateHealth();
	}

	private void EvaluateHealth()
	{
		if (health < 0)
		{
			OnDeath?.Invoke();
		}
	}
}