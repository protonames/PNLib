using System;
using UnityEngine;

namespace PNLib.Time
{
	public class Timer : MonoBehaviour
	{
		[SerializeField]
		private float duration = .25f;

		public float Progress => 1f - (RemainingTime / duration);

		public float RemainingTime => isPaused ? pauseTimeLeft : next - UnityEngine.Time.time;

		public event Action OnCompletedEvent;

		private bool isPaused;
		private float next;
		private float pauseTimeLeft;

		private void Update()
		{
			if (UnityEngine.Time.time <= next)
				return;

			if (isPaused)
				return;

			next = UnityEngine.Time.time + duration;
			OnCompletedEvent?.Invoke();
		}

		public void Restart()
		{
			next = UnityEngine.Time.time + duration;
			isPaused = false;
		}

		public void Pause()
		{
			pauseTimeLeft = next - UnityEngine.Time.time;
			isPaused = true;
		}

		public void Resume()
		{
			next = UnityEngine.Time.time + pauseTimeLeft;
			isPaused = false;
		}

		public void SetDuration(float duration, bool resetTimer = true)
		{
			this.duration = duration;

			if (resetTimer)
				Restart();
		}

		public void Stop()
		{
			Restart();
			Pause();
		}
	}
}