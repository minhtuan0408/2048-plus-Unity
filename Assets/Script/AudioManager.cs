using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Sử dụng Singleton

    public static AudioManager Instance;
    public Sound[] MusicSounds, SFXSounds;
    public AudioSource MusicSource, SFXSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Playing Theme");
    }


    public void PlayMusic(string name)
    {
        Sound soundPlay = Array.Find(MusicSounds, s => s.Name == name);


        if (soundPlay == null)
        {
            print("Not Find Sound");
        }
        else
        {
            MusicSource.clip = soundPlay.Clip;
            MusicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sfxplay = Array.Find(SFXSounds, s => s.Name == name);

        if (sfxplay == null)
        {
            print("Can't find Sound " + name);
        }
        else
        {
            SFXSource.PlayOneShot(sfxplay.Clip);
        }
    }
}