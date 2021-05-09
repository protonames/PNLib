using UnityEngine;

namespace PNLib.Utility
{
	public class PersistentMonoSingleton<T> : MonoSingleton<T>
	where T : MonoBehaviour
	{
		protected override void Awake()
		{
			if (Instance == null)
			{
				Instance = this as T;
				DontDestroyOnLoad(gameObject);
			}
			else if (Instance != this)
			{
				Destroy(gameObject);
			}
			else
			{
				DontDestroyOnLoad(gameObject);
			}
		}
	}
}