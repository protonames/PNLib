using System;
using UnityEngine;

namespace PNLib.Utility
{
	public class DestroyOnCollision : MonoBehaviour
	{
		private void OnCollisionEnter2D()
		{
			Destroy(gameObject);
		}
	}
}