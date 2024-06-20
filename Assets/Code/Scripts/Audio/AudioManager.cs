using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, vfxSounds;
    public AudioSource musicSource, vfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        { 
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("BGM");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x=> x.clipName == name);

        if (sound != null) 
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(vfxSounds, x => x.clipName == name);

        if (sound != null)
        {
            vfxSource.PlayOneShot(sound.clip);
        }
    }
}
