using System.Collections;
using PNLib.Time;
using UnityEngine;

namespace PNLib.Utility
{
	public class IncreaseTimeScale : MonoBehaviour
	{
		[SerializeField]
		private float increaseAmount = .1f;

		[SerializeField]
		private float increaseInterval = 5f;

		private IEnumerator Start()
		{
			while (true)
			{
				yield return new WaitForSecondsRealtime(increaseInterval);

				TimeManager.SetTimeScale(UnityEngine.Time.timeScale + increaseAmount);
			}
		}
	}
}