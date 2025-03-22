using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip musicClip;
    public AudioClip sfxClip;

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

    public void PlayMusic()
    {
        if (musicSource != null && musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(string clipName)
    {
        if (sfxSource != null && sfxClip != null)
        {
            sfxSource.PlayOneShot(sfxClip);
        }
    }
}

