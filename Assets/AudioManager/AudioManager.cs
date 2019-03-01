using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
	
	public static AudioManager instance = null;

	AudioSource m_audioSource;
	AudioData AUDIO;

	#region Mono
	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
		SetupVariable ();
	}

	void SetupVariable() {
		m_audioSource = GetComponent<AudioSource> ();
		m_audioSource.playOnAwake = false;
		AUDIO = GetComponent<AudioData> ();
	}
	#endregion

	#region Controls
	void PLAY(AudioClip clip, float volume = 1.0f) {
		m_audioSource.PlayOneShot (clip, volume);
	}

	public void STOP(){
		if (m_audioSource.isPlaying) {
			m_audioSource.Stop ();
		}
	}
	#endregion

	#region Public
	//Place Different Sound effects here
	public void ShootFireball() {
		PLAY (AUDIO.FireballProjectile);
	}

	#endregion
}
