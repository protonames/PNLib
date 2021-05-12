using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace PNLib.Time
{
	public class RegisterTimerManager : MonoBehaviour
	{
		private List<RegisterTimer> timers = new List<RegisterTimer>();
		private List<RegisterTimer> timersToAdd = new List<RegisterTimer>();

		[UsedImplicitly]
		private void Update()
		{
			UpdateAllTimers();
		}

		public void RegisterTimer(RegisterTimer timer)
		{
			timersToAdd.Add(timer);
		}

		public void CancelAllTimers()
		{
			foreach (RegisterTimer timer in timers)
			{
				timer.Cancel();
			}

			timers = new List<RegisterTimer>();
			timersToAdd = new List<RegisterTimer>();
		}

		public void PauseAllTimers()
		{
			foreach (RegisterTimer timer in timers)
			{
				timer.Pause();
			}
		}

		public void ResumeAllTimers()
		{
			foreach (RegisterTimer timer in timers)
			{
				timer.Resume();
			}
		}

		private void UpdateAllTimers()
		{
			if (timersToAdd.Count > 0)
			{
				timers.AddRange(timersToAdd);
				timersToAdd.Clear();
			}

			foreach (RegisterTimer timer in timers)
			{
				timer.Update();
			}

			timers.RemoveAll(t => t.IsDone);
		}
	}
}