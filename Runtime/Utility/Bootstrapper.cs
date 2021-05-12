using UnityEngine;

namespace PNLib.Utility
{
	public class Bootstrapper : MonoBehaviour
	{
		[SerializeField]
		private int targetFrameRate = 60;

		[SerializeField]
		private VSyncType vSyncCount;

		private void Awake()
		{
			QualitySettings.vSyncCount = (int) vSyncCount;
			Application.targetFrameRate = targetFrameRate;
		}
	}
}