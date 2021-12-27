using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomImageEffect : MonoBehaviour
{
	public Material EffectMaterial;
	public float flashDuration;

	private IEnumerator effectCoroutine;
	private void OnEnable()
	{
		SetEffectAmount(0);
		var playerHealth = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.OnHit += DoEffect;
        }
	}
	private void OnDisable()
	{
		var playerHealth = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.OnHit -= DoEffect;
        }
	}

	public void DoEffect()
	{
		if (effectCoroutine != null)
		{
			StopCoroutine(effectCoroutine);
		}

		effectCoroutine = DoFlash();

		StartCoroutine(effectCoroutine);
	}

	private void SetEffectAmount(float amount)
	{
			EffectMaterial.SetFloat("_Magnitude", amount);
	}
	private IEnumerator DoFlash()
	{
		float lerpTime = 0;

		while (lerpTime < flashDuration)
		{
			lerpTime += Time.deltaTime;
			float perc = lerpTime / flashDuration;

			SetEffectAmount(1f - perc);
			yield return null;
		}
		SetEffectAmount(0);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, EffectMaterial);
	}

}
