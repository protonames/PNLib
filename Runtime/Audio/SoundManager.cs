using System;
using UnityEngine;

namespace PNLib.Audio
{
	public static class SoundManager
	{
		private static GameObject oneShot;
		private static AudioSource audioSource;
		private static float volume = .8f;
		private static bool isInitialized;
		public static event Action OnVolumeChangedEvent;

		public static float Volume
		{
			get
			{
				if (!isInitialized)
				{
					isInitialized = true;
					Volume = PlayerPrefs.GetFloat("SoundVolume", .8f);
				}

				return volume;
			}
			set
			{
				volume = Mathf.Clamp01(value);
				OnVolumeChangedEvent?.Invoke();
				PlayerPrefs.SetFloat("SoundVolume", volume);
			}
		}

		public static void Play(AudioClip clip)
		{
			if (!oneShot)
			{
				oneShot = new GameObject("SoundFX");
			}

			AudioSource oneShotSource = oneShot.AddComponent<AudioSource>();
			oneShotSource.PlayOneShot(clip, Volume);
		}

		public static void Play(AudioClip clip, Vector3 position)
		{
			AudioSource.PlayClipAtPoint(clip, position, Volume);
		}

		public static void Play(AudioClip clip, float volume)
		{
			if (!oneShot)
			{
				oneShot = new GameObject("SoundFX");
			}

			AudioSource oneShotSource = oneShot.AddComponent<AudioSource>();
			oneShotSource.PlayOneShot(clip, volume);
		}
	}
}