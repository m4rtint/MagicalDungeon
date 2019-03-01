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
       getCharacterSource().volume = 0;
       AUDIO = GetComponent<AudioData> ();
	}
	#endregion

	#region Controls
	void PLAYCHARACTER(AudioClip clip, float volume = 1.0f) {
        AudioSource source = getCharacterSource();
        fadeAudio(source, volume);
        source.PlayOneShot (clip, volume);
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
            fadeAudio(source);
        }
    }

    void fadeAudio(AudioSource source, float volume = 0)
    {
        Hashtable ht = new Hashtable();
        ht.Add("audiosource", source);
        ht.Add("volume", volume);
        ht.Add("time", 1f);
        iTween.AudioTo(gameObject, ht);
    }


    void STOPSPELLS()
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
    //Spells
    public void ShootFireball() {
		PLAYSPELL (AUDIO.FireballProjectile);
	}

    public void ActivateHaste()
    {
        PLAYSPELL(AUDIO.Haste);
    }

    public void ActivateFlameCone()
    {
        PLAYSPELL(AUDIO.FlameCone);
    }

    public void ActivateFlameVortex()
    {
        PLAYSPELL(AUDIO.FireVortex, 0.5f);
    }

    public void ActiveIceStorm()
    {
        PLAYSPELL(AUDIO.IceStorm, 0.5f);
    }

    public void StopSpells()
    {
        STOPSPELLS();
    }

    //Character
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
