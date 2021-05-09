using UnityEngine;

namespace PNLib.Utility
{
	public abstract class MonoSingleton<T> : MonoBehaviour
	where T : MonoBehaviour
	{
		protected static T Instance;

		// ReSharper disable once StaticMemberInGenericType
		private static bool isApplicationQuitting;

		protected virtual void Awake()
		{
			if (Instance == null)
				Instance = this as T;
			else if (Instance != this)
				Destroy(gameObject);
		}

		protected virtual void OnApplicationQuit()
		{
			isApplicationQuitting = true;
		}

		public static T GetInstance()
		{
			if (isApplicationQuitting)
				return null;

			if (Instance != null)
				return Instance;

			Instance = FindObjectOfType<T>();

			if (Instance != null)
				return Instance;

			GameObject rootGameObject = new GameObject();
			rootGameObject.name = typeof(T).Name;
			Instance = rootGameObject.AddComponent<T>();
			return Instance;
		}
	}
}