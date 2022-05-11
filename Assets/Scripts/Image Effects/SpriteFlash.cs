using Assets.Scripts.CombatSystem.DamageEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour, IDamageEffect
{

    public Color flashColor;
    public float flashDuration;

    Material mat;

    private IEnumerator flashCoroutine;

    private void Awake()
    {
        mat = GetComponentInChildren<SpriteRenderer>().material;
    }

    private void Start()
    {
        mat.SetColor("_FlashColor", flashColor);
    }

    public void Flash()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = DoFlash();
        StartCoroutine(flashCoroutine);
    }

    private IEnumerator DoFlash()
    {
        float lerpTime = 0;

        while (lerpTime < flashDuration)
        {
            lerpTime += Time.deltaTime;
            float perc = lerpTime / flashDuration;

            SetFlashAmount(1f - perc);
            yield return null;
        }
        SetFlashAmount(0);
    }

    public void EnsureReset()
	{
        SetFlashAmount(0);
	}
    private void SetFlashAmount(float flashAmount)
    {
        mat.SetFloat("_FlashAmount", flashAmount);
    }

    public bool CriticalOnly() => false;

    public void OnTakeDamage(GameObject attacker, float damage)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        Flash();
    }
}