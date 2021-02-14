using Assets.Scripts.Android_UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public void OnPointerDown(PointerEventData eventData)
	{
		AndroidButtonHandler.Instance.BlockPressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		AndroidButtonHandler.Instance.BlockPressed = false;
	}
}
