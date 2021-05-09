using UnityEngine.SceneManagement;

namespace PNLib.Utility
{
	public static class GameSceneManager
	{
		public static void Load(int sceneIndex)
		{
			SceneManager.LoadScene(sceneIndex);
		}

		public static void LoadNext()
		{
			int index = SceneManager.GetActiveScene().buildIndex + 1;
			index %= SceneManager.sceneCountInBuildSettings;
			SceneManager.LoadScene(index);
		}

		public static void Reload()
		{
			int index = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(index);
		}

		public static void ReloadGame()
		{
			SceneManager.LoadScene(0);
		}
	}
}