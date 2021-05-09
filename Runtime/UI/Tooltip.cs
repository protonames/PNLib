using PNLib.Utility;
using TMPro;
using UnityEngine;

namespace PNLib.UI
{
	public class Tooltip : MonoSingleton<Tooltip>
	{
		[SerializeField]
		private TMP_Text tooltipText;

		private RectTransform canvasRect;
		private RectTransform rectTransform;
		private TooltipTimer timer;

		protected override void Awake()
		{
			base.Awake();
			rectTransform = GetComponentInParent<RectTransform>();
			canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
			Hide();
		}

		private void Start()
		{
			Show(tooltipText.text);
		}

		private void Update()
		{
			FollowMouse();

			if (timer == null)
				return;

			timer.Duration -= UnityEngine.Time.deltaTime;

			if (timer.Duration > 0)
				return;

			Hide();
			timer = null;
		}

		public void Show(string text, TooltipTimer tooltipTimer = null)
		{
			timer = tooltipTimer;
			gameObject.SetActive(true);
			tooltipText.SetText(text);
			rectTransform.sizeDelta = tooltipText.GetPreferredValues();
			FollowMouse();
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		private void FollowMouse()
		{
			rectTransform.anchoredPosition = Input.mousePosition / canvasRect.localScale.x;
			rectTransform.anchoredPosition = ClampToScreen(rectTransform.anchoredPosition);
		}

		private Vector2 ClampToScreen(Vector2 anchoredPosition)
		{
			Rect rect = canvasRect.rect;
			float canvasWidth = rect.width;
			float canvasHeight = rect.height;

			if ((anchoredPosition.x + rectTransform.rect.width) > canvasWidth)
				anchoredPosition = new Vector2(canvasWidth - rectTransform.rect.width, anchoredPosition.y);

			if ((anchoredPosition.y + rectTransform.rect.height) > canvasHeight)
				anchoredPosition = new Vector2(anchoredPosition.x, canvasHeight - rectTransform.rect.height);

			if (anchoredPosition.x < 0)
				anchoredPosition = new Vector2(0, anchoredPosition.y);

			if (anchoredPosition.y < 0)
				anchoredPosition = new Vector2(anchoredPosition.x, 0);

			return anchoredPosition;
		}
	}
}