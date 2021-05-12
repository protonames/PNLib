using UnityEngine;

namespace PNLib.Utility
{
	public static class Helper
	{
		private static Camera camera;

		public static Vector3 GetMouseWorldPosition()
		{
			Vector3 worldMousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
			worldMousePosition.z = 0;
			return worldMousePosition;
		}

		public static T GetClosestObjectInRadius<T>(Vector3 origin, float radius)
		{
			Collider2D[] targets = Physics2D.OverlapCircleAll(origin, radius);
			T current = default(T);

			for (int index = 0; index < targets.Length; index++)
			{
				Collider2D hitTarget = targets[index];
				T target = hitTarget.GetComponent<T>();

				if (Equals(target, default(T)))
				{
					continue;
				}

				current = target;
			}

			return current;
		}

		public static float GetAngleFromVector(Vector3 vector)
		{
			return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
		}

		public static Camera Camera
		{
			get
			{
				if (camera)
				{
					return camera;
				}

				camera = Camera.main;
				return camera;
			}
		}
	}
}