using UnityEngine;

namespace PNLib.Utility
{
	public class DestroyAfterSeconds : MonoBehaviour
	{
		[SerializeField]
		private float seconds = 1f;

		private void Start()
		{
			Destroy(gameObject, seconds);
		}
	}
}