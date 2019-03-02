using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	
	public static AudioManager instance = null;

	AudioSource[] audioSources;
	AudioData AUDIO;

    [SerializeField]
    AudioSource backgroundSource;

    enum SOURCES
    {
        FIRESTORM,
        ICESTORM,
        FIRECONE,
        SPELL,
        HEAL,
        CHARACTER,
        ENEMY,
        BOSS,
        GENERIC
    }

    #region Mono
    void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
        SetupAudioSources();
		SetupVariable ();

	}

    private void Start()
    {
        playRandomBackground();
    }

    void playRandomBackground()
    {
        int index = UnityEngine.Random.Range(0, AUDIO.BackgroundMusic.Length);
        backgroundSource.clip = AUDIO.BackgroundMusic[index];
        backgroundSource.loop = true;
        backgroundSource.volume = 0.75f;
        backgroundSource.Play();
    }

    void SetupAudioSources()
    {
        int count = Enum.GetValues(typeof(SOURCES)).Length;
        for (int i = 0; i < count; i++)
        {
            gameObject.AddComponent(typeof(AudioSource));
        }

    }

    void SetupVariable() {
        audioSources = GetComponents<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            source.playOnAwake = false;
        }
       getCharacterSource().volume = 0;
       AUDIO = GetComponent<AudioData> ();
	}
    #endregion

    #region Controls
    void PLAYSPELL(SOURCES source, AudioClip clip, float volume = 1.0f, bool loop = false)
    {
        AudioSource audioSource = audioSources[(int)source];
        audioSource.loop = loop;
        audioSource.PlayOneShot(clip, volume);
    }

    void PLAYCHARACTER(AudioClip clip, float volume = 1.0f) {
        AudioSource audioSource = audioSources[(int)SOURCES.CHARACTER];
        fadeAudio(audioSource, volume);
        audioSource.PlayOneShot (clip, volume);
	}

    void PLAYENEMY(AudioClip clip, float volume = 1.0f)
    {
        AudioSource audioSource = audioSources[(int)SOURCES.ENEMY];
        audioSource.PlayOneShot(clip, volume);
    }

    void PLAYBOSS(AudioClip clip, float volume = 1.0f)
    {
        AudioSource audioSource = audioSources[(int)SOURCES.BOSS];
        audioSource.PlayOneShot(clip, volume);
    }

    void PLAYGENERIC(AudioClip clip, float volume = 1.0f)
    {
        getGenericSource().PlayOneShot(clip, volume);
    }


    void fadeAudio(AudioSource source, float volume = 0, float time = 1f)
    {
        Hashtable ht = new Hashtable();
        ht.Add("audiosource", source);
        ht.Add("volume", volume);
        ht.Add("time", time);
        iTween.AudioTo(gameObject, ht);
    }

    void stopSource(SOURCES source)
    {
        AudioSource audioSource = audioSources[(int)source];
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void STOPALL() 
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
	}

    private AudioSource getCharacterSource()
    {
        return audioSources[(int)SOURCES.CHARACTER];
    }

    private AudioSource getGenericSource()
    {
        return audioSources[(int)SOURCES.GENERIC];
    }

    private AudioSource getHealSource()
    {
        return audioSources[(int)SOURCES.HEAL];
    }
    
    #endregion

    #region Public
    //Spells
    //Fireball
    public void ShootFireball() {
		PLAYSPELL (SOURCES.SPELL, AUDIO.FireballProjectile, 0.5f);
	}

    //Haste
    public void ActivateHaste()
    {
        PLAYSPELL(SOURCES.SPELL, AUDIO.Haste);
    }

    //FireCone
    public void PlayFireCone()
    {
        AudioSource audioSource = audioSources[(int)SOURCES.FIRECONE];
        if (!audioSource.isPlaying)
        {
            PLAYSPELL(SOURCES.FIRECONE, AUDIO.FlameCone, 0.25f, true);
        }
    }

    public void StopFireCone()
    {
        stopSource(SOURCES.FIRECONE);
    }

    //Fire Storm
    public void PlayFireStorm()
    {
        PLAYSPELL(SOURCES.FIRESTORM, AUDIO.FireStorm, 0.5f);
    }

    public void StopFireStorm()
    {
        stopSource(SOURCES.FIRESTORM);
    }

    //IceStorm
    public void PlayIceStorm()
    {
        PLAYSPELL(SOURCES.ICESTORM, AUDIO.IceStorm, 0.5f);
    }

    public void StopIceStorm()
    {
        stopSource(SOURCES.ICESTORM);
    }

    //Generic Spell
    public void StopSpells()
    {
        stopSource(SOURCES.SPELL);
    }

    //Character
    public void PlayHealPlayer()
    {
        fadeAudio(getHealSource(), 0.5f);
        getHealSource().PlayOneShot (AUDIO.Healing);
    }

    public void StopHealPlayer()
    {
        fadeAudio(getHealSource(), 0f);
    }

    public void PlayHurtPlayer()
    {
        int index = UnityEngine.Random.Range(0, AUDIO.PlayerHurt.Length);
        PLAYCHARACTER(AUDIO.PlayerHurt[index], 1.0f);
    }

    public void PlayPlayerDeath()
    {
        PLAYCHARACTER(AUDIO.PlayerDeath, 1.0f);
    }

    //Enemy
    public void PlayMushroomHurt()
    {
        int index = UnityEngine.Random.Range(0, AUDIO.MushroomHurt.Length);
        PLAYENEMY(AUDIO.MushroomHurt[index], 0.5f);
    }

    public void PlayMushroomDeath()
    {
        int index = UnityEngine.Random.Range(0, AUDIO.MushroomDeath.Length);
        PLAYENEMY(AUDIO.MushroomDeath[index], 0.5f);
    }

    public void PlayZombieHurt()
    {
        int index = UnityEngine.Random.Range(0, AUDIO.ZombieHurt.Length);
        PLAYENEMY(AUDIO.ZombieHurt[index], 0.5f);
    }

    public void PlayZombieDeath()
    {
        int index = UnityEngine.Random.Range(0, AUDIO.ZombieDeath.Length);
        PLAYENEMY(AUDIO.ZombieDeath[index], 0.5f);
    }

    //Boss
    public void PlayTreeBossHurt()
    {
        int index = UnityEngine.Random.Range(0, AUDIO.TreeBossHurt.Length);
        PLAYENEMY(AUDIO.TreeBossHurt[index], 0.5f);
    }

    //UNIQUE/GENERIC
    public void PlayItemHealingPickup()
    {
        PLAYGENERIC(AUDIO.HealingItem, 1.0f);
    }

    #endregion
}
