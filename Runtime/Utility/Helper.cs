using System.Collections.Generic;
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

		public static bool GetClosestObjectInSphereRadius<T>(Vector3 origin, float radius, out T current)
		{
			Collider[] targets = Physics.OverlapSphere(origin, radius);
			current = default(T);
			float maxDistance = float.MaxValue;

			for (int index = 0; index < targets.Length; index++)
			{
				Collider hitTarget = targets[index];
				T target = hitTarget.GetComponent<T>();

				if (Equals(target, default(T)))
				{
					continue;
				}

				float currentDistance = Vector3.Distance(origin, hitTarget.transform.position);

				if (currentDistance < maxDistance)
				{
					current = target;
					maxDistance = currentDistance;
				}
			}

			return !Equals(current, default(T));
		}

		public static bool GetClosestObjectInCircleRadius<T>(Vector3 origin, float radius, out T current)
		{
			Collider2D[] targets = Physics2D.OverlapCircleAll(origin, radius);
			current = default(T);
			float maxDistance = float.MaxValue;

			for (int index = 0; index < targets.Length; index++)
			{
				Collider2D hitTarget = targets[index];
				T target = hitTarget.GetComponent<T>();

				if (Equals(target, default(T)))
				{
					continue;
				}

				float currentDistance = Vector3.Distance(origin, hitTarget.transform.position);

				if (currentDistance < maxDistance)
				{
					current = target;
					maxDistance = currentDistance;
				}
			}

			return !Equals(current, default(T));
		}

		public static bool GetAllObjectsInCircleRadius<T>(Vector3 origin, float radius, out List<T> currentList)
		{
			Collider2D[] targets = Physics2D.OverlapCircleAll(origin, radius);
			currentList = new List<T>();

			for (int index = 0; index < targets.Length; index++)
			{
				Collider2D hitTarget = targets[index];
				T target = hitTarget.GetComponent<T>();

				if (Equals(target, default(T)))
				{
					continue;
				}

				currentList.Add(target);
			}

			return currentList.Count > 0;
		}
	}
}