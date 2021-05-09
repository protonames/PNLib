using UnityEngine;

namespace PNLib.Utility
{
	public class FixedRotation : MonoBehaviour
	{
		private Quaternion rotation;

		private void Awake()
		{
			rotation = transform.rotation;
		}

		private void LateUpdate()
		{
			transform.rotation = rotation;
		}
	}
}