using System.Collections;
using PNLib.Utility;
using UnityEngine;

namespace PNLib.Time
{
	public class TimeManager : MonoSingleton<TimeManager>
	{
		private const float FixedDeltaTime = .02f;
		private const float TimeScale = 1f;

		private void OnDestroy()
		{
			UnityEngine.Time.timeScale = TimeScale;
			UnityEngine.Time.fixedDeltaTime = FixedDeltaTime;
		}

		public void Freeze(float seconds)
		{
			StartCoroutine(FreezeTimeRoutine(seconds));
		}

		public static void Pause()
		{
			UnityEngine.Time.timeScale = 0f;
		}

		public static void Resume()
		{
			UnityEngine.Time.timeScale = TimeScale;
		}

		private static IEnumerator FreezeTimeRoutine(float duration)
		{
			UnityEngine.Time.timeScale = .02f;
			UnityEngine.Time.fixedDeltaTime = UnityEngine.Time.timeScale * FixedDeltaTime;
			yield return new WaitForSecondsRealtime(duration);

			UnityEngine.Time.timeScale = TimeScale;
			UnityEngine.Time.fixedDeltaTime = FixedDeltaTime;
		}
	}
}