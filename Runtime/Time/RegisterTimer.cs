using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PNLib.Time
{
	public class RegisterTimer
	{
		public float Duration { get; }

		public bool IsLooped { get; }

		public bool IsCompleted { get; private set; }

		public bool UsesRealTime { get; }

		public bool IsPaused => timeElapsedBeforePause.HasValue;

		public bool IsCancelled => timeElapsedBeforeCancel.HasValue;

		public bool IsDone => IsCompleted || IsCancelled || IsOwnerDestroyed;

		public static RegisterTimer Register(float duration, Action onComplete, Action<float> onUpdate = null,
			bool isLooped = false, bool useRealTime = false, MonoBehaviour autoDestroyOwner = null)
		{
			if (!manager)
			{
				RegisterTimerManager managerInScene = Object.FindObjectOfType<RegisterTimerManager>();

				if (managerInScene)
				{
					manager = managerInScene;
				}
				else
				{
					GameObject managerObject = new GameObject
					{
						name = "TimerManager",
					};

					manager = managerObject.AddComponent<RegisterTimerManager>();
				}
			}

			RegisterTimer registerTimer = new RegisterTimer(
				duration,
				onComplete,
				onUpdate,
				isLooped,
				useRealTime,
				autoDestroyOwner
			);

			manager.RegisterTimer(registerTimer);
			return registerTimer;
		}

		public static void Cancel(RegisterTimer registerTimer)
		{
			registerTimer?.Cancel();
		}

		public static void Pause(RegisterTimer registerTimer)
		{
			registerTimer?.Pause();
		}

		public static void Resume(RegisterTimer registerTimer)
		{
			registerTimer?.Resume();
		}

		public static void CancelAllRegisteredTimers()
		{
			if (manager)
				manager.CancelAllTimers();
		}

		public static void PauseAllRegisteredTimers()
		{
			if (manager)
				manager.PauseAllTimers();
		}

		public static void ResumeAllRegisteredTimers()
		{
			if (manager)
				manager.ResumeAllTimers();
		}

		public void Cancel()
		{
			if (IsDone)
				return;

			timeElapsedBeforeCancel = GetTimeElapsed();
			timeElapsedBeforePause = null;
		}

		public void Pause()
		{
			if (IsPaused || IsDone)
				return;

			timeElapsedBeforePause = GetTimeElapsed();
		}

		public void Resume()
		{
			if (!IsPaused || IsDone)
				return;

			timeElapsedBeforePause = null;
		}

		public float GetTimeElapsed()
		{
			if (IsCompleted || GetWorldTime() >= GetFireTime())
				return Duration;

			return timeElapsedBeforeCancel ?? timeElapsedBeforePause ?? (GetWorldTime() - startTime);
		}

		public float GetTimeRemaining()
		{
			return Duration - GetTimeElapsed();
		}

		public float GetRatioComplete()
		{
			return GetTimeElapsed() / Duration;
		}

		public float GetRatioRemaining()
		{
			return GetTimeRemaining() / Duration;
		}

		private static RegisterTimerManager manager;

		private bool IsOwnerDestroyed => hasAutoDestroyOwner && !autoDestroyOwner;

		private readonly Action onComplete;
		private readonly Action<float> onUpdate;
		private float startTime;
		private float lastUpdateTime;
		private float? timeElapsedBeforeCancel;
		private float? timeElapsedBeforePause;
		private readonly MonoBehaviour autoDestroyOwner;
		private readonly bool hasAutoDestroyOwner;

		private RegisterTimer(float duration, Action onComplete, Action<float> onUpdate,
			bool isLooped, bool usesRealTime, MonoBehaviour autoDestroyOwner)
		{
			Duration = duration;
			this.onComplete = onComplete;
			this.onUpdate = onUpdate;
			IsLooped = isLooped;
			UsesRealTime = usesRealTime;
			this.autoDestroyOwner = autoDestroyOwner;
			hasAutoDestroyOwner = autoDestroyOwner;
			startTime = GetWorldTime();
			lastUpdateTime = startTime;
		}

		private float GetWorldTime()
		{
			return UsesRealTime ? UnityEngine.Time.realtimeSinceStartup : UnityEngine.Time.time;
		}

		private float GetFireTime()
		{
			return startTime + Duration;
		}

		private float GetTimeDelta()
		{
			return GetWorldTime() - lastUpdateTime;
		}

		public void Update()
		{
			if (IsDone)
				return;

			if (IsPaused)
			{
				startTime += GetTimeDelta();
				lastUpdateTime = GetWorldTime();
				return;
			}

			lastUpdateTime = GetWorldTime();
			onUpdate?.Invoke(GetTimeElapsed());

			if (GetWorldTime() >= GetFireTime())
			{
				onComplete?.Invoke();

				if (IsLooped)
					startTime = GetWorldTime();
				else
					IsCompleted = true;
			}
		}
	}
}