using UnityEngine;

namespace PNLib.Utility
{
	public static class Extensions
	{
		public static Vector3 DirectionTo(this Vector3 from, Vector3 to)
		{
			return (to - from).normalized;
		}
	}
}