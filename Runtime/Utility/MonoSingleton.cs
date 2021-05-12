using UnityEngine;

namespace PNLib.Utility
{
	public abstract class MonoSingleton<T> : SingletonCollection
	where T : MonoBehaviour
	{
		[SerializeField]
		private bool isPersistent;

		protected virtual void Awake()
		{
			if (!Instances.TryGetValue(typeof(T), out object instance))
			{
				instance = this as T;
				Instances.Add(typeof(T), instance);

				if (isPersistent)
				{
					DontDestroyOnLoad(gameObject);
				}
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public static T GetInstance()
		{
			if (IsApplicationQuitting)
			{
				return null;
			}

			if (Instances.TryGetValue(typeof(T), out object instance))
			{
				return (T) instance;
			}

			instance = FindObjectOfType<T>();

			if (instance != null)
			{
				return (T) instance;
			}

			GameObject rootGameObject = new GameObject();
			rootGameObject.name = typeof(T).Name;
			instance = rootGameObject.AddComponent<T>();
			return (T) instance;
		}
	}
}