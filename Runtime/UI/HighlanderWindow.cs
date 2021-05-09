using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PNLib.UI
{
	public class HighlanderWindow : MonoBehaviour
	{
		public bool IsVisible;
		public int Order;
		private static readonly List<HighlanderWindow> Windows = new List<HighlanderWindow>();

		private void Awake()
		{
			Windows.Add(this);
		}

		private void OnDestroy()
		{
			Windows.Remove(this);
		}

		public void Show()
		{
			SetVisible(true);
		}

		public void Hide()
		{
			SetVisible(false);
		}

		public void Toggle()
		{
			SetVisible(!IsVisible);
		}

		private void SetVisible(bool visible)
		{
			if (visible)
				foreach (HighlanderWindow menu in Windows.Where(
					menu => menu != this && menu.IsVisible && menu.Order >= Order
				))
				{
					menu.SetVisible(false);
				}

			IsVisible = visible;
			gameObject.SetActive(IsVisible);
		}
	}
}