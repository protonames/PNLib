using UnityEngine;

namespace PNLib.Utility
{
	public class LoadSceneOnCollision : MonoBehaviour
	{
		[SerializeField]
		private LoadType loadType;

		private enum LoadType { Current, Next, Restart }

		private void OnCollisionEnter2D()
		{
			switch (loadType)
			{
				case LoadType.Current:
					GameSceneManager.Reload();
					break;
				case LoadType.Next:
					GameSceneManager.LoadNext();
					break;
				case LoadType.Restart:
					GameSceneManager.ReloadGame();
					break;
			}
		}
	}
}