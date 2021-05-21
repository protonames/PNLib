using UnityEngine;

namespace SJ13.Speed_Run
{
	public class BouncePad : MonoBehaviour
	{
		private void OnCollisionEnter2D(Collision2D other)
		{
			Rigidbody2D otherRigidbody = other.collider.GetComponent<Rigidbody2D>();

			if (otherRigidbody)
			{
				Vector2 normal = other.contacts[0].normal;

				Vector2 velocity = (-2f * Vector2.Dot(otherRigidbody.velocity, normal) * normal)
									+ otherRigidbody.velocity;

				otherRigidbody.velocity = velocity;
			}
		}
	}
}