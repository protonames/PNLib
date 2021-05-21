using UnityEngine;

namespace PNLib.Movement
{
	public class Teleport : MonoBehaviour
	{
		public void TeleportTo(Vector3 point)
		{
			transform.position = point;
		}
	}
}