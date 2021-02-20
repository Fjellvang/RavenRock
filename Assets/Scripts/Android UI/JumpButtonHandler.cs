using Assets.Scripts.Android_UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("Pressed Jump");
		AndroidButtonHandler.Instance.JumpButton = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Debug.Log("Released Jump");
		AndroidButtonHandler.Instance.JumpButton = false;
	}
}
