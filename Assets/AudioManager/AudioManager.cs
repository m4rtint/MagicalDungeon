using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
	
	public static AudioManager instance = null;

	AudioSource[] m_audioSource;
	AudioData AUDIO;

    enum SOURCES
    {
        SPELL1,
        SPELL2,
        CHARACTER,
        GENERIC
    }

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
        m_audioSource = GetComponents<AudioSource>();
        foreach (AudioSource source in m_audioSource)
        {
            source.playOnAwake = false;
        }
		AUDIO = GetComponent<AudioData> ();
	}
	#endregion

	#region Controls
	void PLAYCHARACTER(AudioClip clip, float volume = 1.0f) {

		getCharacterSource().PlayOneShot (clip, volume);
	}

    void PLAYSPELL(AudioClip clip, float volume = 1.0f)
    {
        getSpellSource().PlayOneShot(clip, volume);
    }

    void PLAYGENERIC(AudioClip clip, float volume = 1.0f)
    {
        getGenericSource().PlayOneShot(clip, volume);
    }

    public void STOPCHARACTER()
    {
        AudioSource source = m_audioSource[(int)SOURCES.CHARACTER];
        if (source.isPlaying)
        {
            source.Stop();
        }
    }

    public void STOPSPELLS()
    {
        AudioSource source = m_audioSource[(int)SOURCES.SPELL1];
        AudioSource source2 = m_audioSource[(int)SOURCES.SPELL2];
        if (source.isPlaying)
        {
            source.Stop();
        }

        if (source2.isPlaying)
        {
            source2.Stop();
        }
    }

    public void STOPGENERIC()
    {
        AudioSource source = m_audioSource[(int)SOURCES.GENERIC];
        if (source.isPlaying)
        {
            source.Stop();
        }
    }

    public void STOPALL() 
    {
        foreach (AudioSource source in m_audioSource)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
	}

    private AudioSource getSpellSource()
    {
        return m_audioSource[(int)SOURCES.SPELL1].isPlaying ? m_audioSource[(int)SOURCES.SPELL2] : m_audioSource[(int)SOURCES.SPELL1];
    }

    private AudioSource getCharacterSource()
    {
        return m_audioSource[(int)SOURCES.CHARACTER];
    }

    private AudioSource getGenericSource()
    {
        return m_audioSource[(int)SOURCES.GENERIC];
    }

    #endregion

    #region Public
    //Place Different Sound effects here
    public void ShootFireball() {
		PLAYSPELL (AUDIO.FireballProjectile);
	}

    public void PlayHealPlayer()
    {
        PLAYCHARACTER(AUDIO.Healing);
    }

    public void StopHealPlayer()
    {
        STOPCHARACTER();
    }

    #endregion
}
