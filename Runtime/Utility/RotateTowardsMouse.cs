using PNLib.Utility;
using UnityEngine;

namespace SJ13.Speed_Run
{
	public class RotateTowardsMouse : MonoBehaviour
	{
		private void Update()
		{
			Vector3 mouseWorldPosition = Helper.GetMouseWorldPosition();
			float angle = Helper.GetAngleFromVector(transform.position.DirectionTo(mouseWorldPosition));
			transform.localEulerAngles = new Vector3(0, 0, angle);
		}
	}
}