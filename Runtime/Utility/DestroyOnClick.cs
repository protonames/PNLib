using UnityEngine;

namespace PNLib.Utility
{
	public class DestroyOnClick : MonoBehaviour
	{
		private void OnMouseDown()
		{
			Destroy(gameObject);
		}
	}
}