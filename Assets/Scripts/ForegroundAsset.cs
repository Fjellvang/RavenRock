using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class ForegroundAsset : MonoBehaviour
{

    SpriteRenderer renderer;
	// Start is called before the first frame update
	void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log($"Ontrigger enter: {collision.name}", this);
        renderer.material.SetFloat("_Alpha", 0.5f);
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
        Debug.Log($"Ontrigger exit: {collision.name}", this);
        renderer.material.SetFloat("_Alpha", 1);
	}
}
