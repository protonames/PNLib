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
			SetTimeScale(0);
		}

		public static void Resume()
		{
			UnityEngine.Time.timeScale = TimeScale;
		}

		public static void SetTimeScale(float value)
		{
			UnityEngine.Time.timeScale = value;
			UnityEngine.Time.fixedDeltaTime = 0.02F * value;
		}

		public static void ResetTimeScale()
		{
			SetTimeScale(TimeScale);
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