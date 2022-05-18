using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class ForegroundAsset : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
	// Start is called before the first frame update
	void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        //spriteRenderer.color = new Color(1, 1, 1, .5f);
        StopAllCoroutines();
        StartCoroutine(LerpCorotine(0.1f, x => Mathf.Lerp(spriteRenderer.color.a, 0.5f, x)));
        spriteRenderer.material.SetFloat("_Alpha", 0.5f);
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
        StopAllCoroutines();
        StartCoroutine(LerpCorotine(0.1f, x => Mathf.Lerp(spriteRenderer.color.a, 1, x)));
        spriteRenderer.material.SetFloat("_Alpha", 1);
	}

    IEnumerator LerpCorotine(float seconds, Func<float, float> lerp)
    {
        var inc = 0f;
        float value;
        do
        {
            yield return new WaitForSeconds(seconds);
            inc += seconds;
            value = lerp(inc);
            spriteRenderer.color = new Color(1,1,1, value);
        } while (value > 0.5f);
    }
}
