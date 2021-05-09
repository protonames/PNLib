using UnityEngine;

namespace PNLib.Math
{
	public class Parabola
	{
		public static ParabolaInfo Calculate(Vector3 start, Vector3 end, float height)
		{
			if (height <= end.y)
				height = end.y;

			if (height <= start.y)
				height = start.y;

			float yDisplacement = end.y - start.y;
			Vector3 xzDisplacement = new Vector3(end.x - start.x, 0, end.z - start.z);

			float travelTime = Mathf.Sqrt((-2 * height) / Physics.gravity.y)
								+ Mathf.Sqrt((2 * (yDisplacement - height)) / Physics.gravity.y);

			Vector3 verticalVelocity = Vector3.up * Mathf.Sqrt(-2 * Physics.gravity.y * height);
			Vector3 groundVelocity = xzDisplacement / travelTime;

			return new ParabolaInfo
			{
				Origin = start,
				Velocity = verticalVelocity + groundVelocity,
				Duration = travelTime,
			};
		}

		public static Vector3[] GetCurvePoints(ParabolaInfo info, int pointCount = 30)
		{
			Vector3[] points = new Vector3[pointCount + 1];
			points[0] = info.Origin;

			for (int i = 1; i <= pointCount; i++)
			{
				float delta = (i / (float) pointCount) * info.Duration;
				Vector3 displacement = (info.Velocity * delta) + ((Physics.gravity * delta * delta) / 2f);
				Vector3 drawPoint = info.Origin + displacement;
				points[i] = drawPoint;
			}

			return points;
		}

		public class ParabolaInfo
		{
			public Vector3 Origin;
			public Vector3 Velocity;
			public float Duration;
		}
	}
}