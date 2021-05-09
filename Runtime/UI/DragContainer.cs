using UnityEngine;

namespace PNLib.UI
{
	public class DragContainer : MonoBehaviour
	{
		public Transform GetChild()
		{
			return transform.childCount > 0 ? transform.GetChild(0) : null;
		}
	}
}