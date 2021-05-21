using UnityEngine;

namespace PNLib.Movement
{
	public class ConstantVelocity : MonoBehaviour
	{
		[SerializeField]
		private Vector3 direction;

		[SerializeField]
		private float speed = 8f;

		private Rigidbody2D rb;

		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			rb.velocity = direction * speed;
		}
	}
}