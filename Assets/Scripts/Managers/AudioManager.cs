using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{


    public static AudioManager Instance { get; private set; }
    [SerializeField] AudioMixer master;
    [SerializeField] AudioSource sfxAudio, musicAudio;
    public Sound[] musicSounds, sfxSounds;

    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        PlayMusic("Menu Theme");
    }

    public void PlaySFX(string name)
    {
        Sound sfx = Array.Find(sfxSounds, x => x.nameSound == name);

        if (sfx == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxAudio.PlayOneShot(sfx.clip);
        }
    }

    public void PlayMusic(string name)
    {
        Sound music = Array.Find(musicSounds, x => x.nameSound == name);

        if (music == null)
        {
            Debug.Log("Sound Not Found");
            Debug.Log(music);
        }

        else
        {
            musicAudio.clip = music.clip;
            musicAudio.Play();
            musicAudio.loop = true;
        }
    }

    public void RestartMusic()
{
    musicAudio.Stop();
    musicAudio.Play();
}

}