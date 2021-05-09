using UnityEngine;

namespace PNLib.Utility
{
	public class Bootstrapper : MonoBehaviour
	{
		[SerializeField]
		private int targetFrameRate = 60;

		[SerializeField]
		private VSyncType vSyncCount;

		private enum VSyncType
		{
			DontSync = 0, EveryVBlank = 1, EverySecondVBlank = 2,
		}

		private void Awake()
		{
			QualitySettings.vSyncCount = (int) vSyncCount;
			Application.targetFrameRate = targetFrameRate;
		}
	}
}