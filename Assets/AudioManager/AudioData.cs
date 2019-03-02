using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioData : MonoBehaviour {

    //Audio Clips saved here
    [Header("Background")]
    public AudioClip[] BackgroundMusic;

    [Header("Spells")]
	public AudioClip FireballProjectile;
    public AudioClip Haste;
    public AudioClip FlameCone;
    public AudioClip FireStorm;
    public AudioClip IceStorm;

    [Header("Character")]
    public AudioClip Healing;
    public AudioClip[] PlayerHurt;
    public AudioClip PlayerDeath;
}
