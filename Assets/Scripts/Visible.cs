using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visible : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
	{
		SetWithinRange(collision, true);
	}

	private void OnTriggerExit2D(Collider2D collision)
    {
		SetWithinRange(collision, false);
    }
	private static void SetWithinRange(Collider2D collision, bool withinRange)
	{
		if ((collision.tag == "Enemy" || collision.tag == "Monk") && collision.gameObject.TryGetComponent<AI>(out var ai))
		{
			ai.witinRange = withinRange;
		}
	}
}
