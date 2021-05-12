using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PNLib.Utility
{
	public class MouseEnterExitEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		public event Action OnMouseEnterEvent;
		public event Action OnMouseExitEvent;

		public void OnPointerEnter(PointerEventData eventData)
		{
			OnMouseEnterEvent?.Invoke();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			OnMouseExitEvent?.Invoke();
		}
	}
}