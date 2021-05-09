using System;
using System.Collections.Generic;
using PNLib.Utility;
using UnityEngine;

namespace PNLib.Audio
{
	[RequireComponent(typeof(AudioSource))]
	public class MusicManager : MonoSingleton<MusicManager>
	{
		[SerializeField]
		private List<AudioClip> playlist = new List<AudioClip>();

		public static event Action OnVolumeChangedEvent;

		public static float Volume
		{
			get
			{
				if (!initialized)
				{
					initialized = true;
					Volume = PlayerPrefs.GetFloat("MusicVolume", .8f);
				}

				return volume;
			}
			set
			{
				volume = Mathf.Clamp01(value);
				OnVolumeChangedEvent?.Invoke();
				PlayerPrefs.SetFloat("MusicVolume", volume);
			}
		}

		private static bool initialized;
		private static float volume = .8f;
		private AudioSource audioSource;
		private int musicIndex = -1;

		protected override void Awake()
		{
			base.Awake();
			audioSource = GetComponent<AudioSource>();
		}

		private void Start()
		{
			UpdateVolume();

			if (playlist.Count == 1)
			{
				audioSource.loop = true;
				audioSource.clip = playlist[0];
				audioSource.Play();
			}
			else
			{
				PlayNext();
			}
		}

		private void OnEnable()
		{
			OnVolumeChangedEvent += UpdateVolume;
		}

		private void OnDisable()
		{
			OnVolumeChangedEvent -= UpdateVolume;
		}

		public void PlayNext()
		{
			CancelInvoke(nameof(PlayNext));
			musicIndex++;
			musicIndex %= playlist.Count;
			audioSource.clip = playlist[musicIndex];
			audioSource.Play();
			Invoke(nameof(PlayNext), playlist[musicIndex].length);
		}

		private void UpdateVolume()
		{
			audioSource.volume = Volume;
		}
	}
}