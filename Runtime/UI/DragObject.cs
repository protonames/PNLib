using UnityEngine;
using UnityEngine.EventSystems;

namespace PNLib.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		private CanvasGroup canvasGroup;
		private DragContainer source;

		public void OnBeginDrag(PointerEventData eventData)
		{
			canvasGroup.blocksRaycasts = false;
			source = GetComponentInParent<DragContainer>();
		}

		public void OnDrag(PointerEventData eventData)
		{
			transform.position = eventData.position;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.pointerEnter)
			{
				DragContainer destination = eventData.pointerEnter.GetComponentInParent<DragContainer>();

				if (!destination)
					return;

				if (ReferenceEquals(destination, source))
					return;

				Transform swap = destination.GetChild();
				transform.SetParent(destination.transform, false);

				if (swap)
					swap.SetParent(source.transform, false);
			}

			transform.localPosition = Vector3.zero;
			canvasGroup.blocksRaycasts = true;
		}

		private void Awake()
		{
			canvasGroup = GetComponent<CanvasGroup>();
		}
	}
}