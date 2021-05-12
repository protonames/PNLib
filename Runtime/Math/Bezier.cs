using UnityEngine;

namespace PNLib.Math
{
	[RequireComponent(typeof(LineRenderer))]
	public class Bezier : MonoBehaviour
	{
		[SerializeField]
		private Vector3 controlPoint;

		[SerializeField]
		private Vector3 end;

		[SerializeField]
		private Vector3 start;

		private int interpolationCount = 50;
		private LineRenderer lineRenderer;
		private Vector3[] positions;

		private void Awake()
		{
			lineRenderer = GetComponent<LineRenderer>();
		}

		public void SetCurve(Vector3 start, Vector3 control, Vector3 end)
		{
			SetCurve(start, control, end, 50);
		}

		public void SetCurve(Vector3 start, Vector3 control, Vector3 end, int pointCount)
		{
			this.start = start;
			controlPoint = control;
			this.end = end;
			interpolationCount = pointCount;
			positions = new Vector3[interpolationCount];
			lineRenderer.positionCount = interpolationCount;
		}

		public void DrawCurve()
		{
			for (int i = 1; i < (interpolationCount + 1); i++)
			{
				float delta = i / (float) interpolationCount;
				positions[i - 1] = CalculateQuadraticBezierPoint(delta, start, controlPoint, end);
			}

			lineRenderer.SetPositions(positions);
		}

		private static Vector3 CalculateQuadraticBezierPoint(float delta, Vector3 startPoint, Vector3 controlPoint,
			Vector3 endPoint)
		{
			float inversedDelta = 1 - delta;
			float squaredDelta = delta * delta;
			float squaredInversedDelta = inversedDelta * inversedDelta;
			Vector3 point = squaredInversedDelta * startPoint;
			point += 2 * inversedDelta * delta * controlPoint;
			point += squaredDelta * endPoint;
			return point;
		}
	}
}