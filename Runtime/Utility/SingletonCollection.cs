using System;
using System.Collections.Generic;
using UnityEngine;

namespace PNLib.Utility
{
	public class SingletonCollection : MonoBehaviour
	{
		protected static readonly Dictionary<Type, object> Instances = new Dictionary<Type, object>();
		protected static bool IsApplicationQuitting;

		protected virtual void OnApplicationQuit()
		{
			SetApplicationQuit();
		}

		private static void SetApplicationQuit()
		{
			IsApplicationQuitting = true;
		}
	}
}