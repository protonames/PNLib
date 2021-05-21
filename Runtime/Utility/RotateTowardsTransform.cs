using PNLib.Utility;
using UnityEngine;

namespace SJ13.Speed_Run
{
	public class RotateTowardsTransform : MonoBehaviour
	{
		[SerializeField]
		private float rotationSpeed;

		[SerializeField]
		private Transform target;

		private void Update()
		{
			float targetAngle = Helper.GetAngleFromVector(transform.position.DirectionTo(target.position));
			Vector3 eulerAngles = new Vector3(0, 0, targetAngle);

			transform.eulerAngles = Vector3.Lerp(
				transform.eulerAngles,
				eulerAngles,
				(1f / rotationSpeed) * Time.deltaTime
			);
		}
	}
}