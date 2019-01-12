using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	[SerializeField] private AudioSource efxSource;
	[SerializeField] private AudioSource musicSource;
	public static SoundManager Instance = null;

	[SerializeField] private float lowPitchRange = .95f;
	[SerializeField] private float highPitchRange = 1.05f;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);


	}
	
	public void PlaySingle (AudioClip clip) {
		efxSource.loop = false;
		efxSource.clip = clip;
		efxSource.Play ();
	}

	public void PlaySingleLoop (AudioClip clip) {
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
