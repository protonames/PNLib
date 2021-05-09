using System;
using PNLib.Time;
using UnityEngine;
using UnityEngine.UI;

namespace PNLib.UI
{
	public class HoverButton : MonoBehaviour
	{
		[SerializeField]
		private Button button;

		[SerializeField]
		private GameObject container;

		[SerializeField]
		private Timer hideTimer;

		public event Action OnButtonClickedEvent;

		private void Start()
		{
			button.onClick.AddListener(() => OnButtonClickedEvent?.Invoke());
			hideTimer.OnCompletedEvent += Hide;
			Hide();
		}

		private void OnDisable()
		{
			hideTimer.OnCompletedEvent -= Hide;
		}

		public void HideCountdown()
		{
			hideTimer.Restart();
		}

		public void Show()
		{
			container.SetActive(true);
		}

		public void Hide()
		{
			hideTimer.Stop();
			container.SetActive(false);
		}
	}
}