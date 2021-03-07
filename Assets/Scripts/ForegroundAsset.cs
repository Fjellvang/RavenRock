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
        Debug.Log($"Ontrigger enter: {collision.name}", this);
        spriteRenderer.material.SetFloat("_Alpha", 0.5f);
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
        Debug.Log($"Ontrigger exit: {collision.name}", this);
        spriteRenderer.material.SetFloat("_Alpha", 1);
	}
}
