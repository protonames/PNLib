using UnityEngine;

namespace SJ13
{
	public class Launchable : MonoBehaviour
	{
		private Vector3 direction;
		private new Rigidbody2D rigidbody;
		private float speed;

		private void Awake()
		{
			rigidbody = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			rigidbody.velocity = speed * direction;
		}

		public void Launch(Vector3 direction)
		{
			Launch(speed, direction);
		}

		public void Launch(float speed, Vector3 direction)
		{
			this.speed = speed;
			this.direction = direction;
		}
	}
}