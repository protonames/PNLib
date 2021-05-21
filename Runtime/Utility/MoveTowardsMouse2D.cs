using PNLib.Utility;
using UnityEngine;

namespace PNLib
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class MoveTowardsMouse2D : MonoBehaviour
	{
		[SerializeField]
		private float moveSpeed = 5f;

		private new Rigidbody2D rigidbody;

		private void Awake()
		{
			rigidbody = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			rigidbody.velocity = transform.position.DirectionTo(Helper.GetMouseWorldPosition()) * moveSpeed;
		}
	}
}