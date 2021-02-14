using Assets.Scripts.Android_UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public void OnPointerDown(PointerEventData eventData)
	{
		AndroidButtonHandler.Instance.AttackPressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		AndroidButtonHandler.Instance.AttackPressed = false;
	}

}
