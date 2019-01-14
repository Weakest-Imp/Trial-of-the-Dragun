using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	[SerializeField] private AudioSource efxSource;
	[SerializeField] private AudioSource musicSource;
	public static SoundManager Instance = null;

	[SerializeField] private float lowPitchRange = .95f;
	[SerializeField] private float highPitchRange = 1.05f;
	[SerializeField] private float stopTime = 2;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);


	}

	public void StopBGM () {
		StartCoroutine (StopBGMCoroutine ());
	}
	IEnumerator	StopBGMCoroutine () {
		float factor = 1/stopTime;
		float time = 0;
		float delta = 0;
		while (time < stopTime) {
			yield return null;
			delta = Time.deltaTime;
			musicSource.volume -= delta * factor;
			time += delta;
		}
		musicSource.Stop ();
		musicSource.volume = 1;
	}

	public void PlayBGM (AudioClip clip) {
		musicSource.loop = true;
		musicSource.clip = clip;
		musicSource.Play ();
	}

	public void PlayJingle (AudioClip clip) {
		musicSource.loop = false;
		musicSource.clip = clip;
		musicSource.Play ();
	}

	public void PlaySFX (AudioClip clip) {
		efxSource.loop = false;
		efxSource.clip = clip;
		efxSource.Play ();
	}

	public void PlaySFXLoop (AudioClip clip) {
		efxSource.loop = true;
		efxSource.clip = clip;
		efxSource.Play ();
	}

	public void RandomizeSFX (params AudioClip [] clips) {
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		efxSource.pitch = randomPitch;
		efxSource.clip = clips [randomIndex];
		efxSource.Play ();
	}

}
