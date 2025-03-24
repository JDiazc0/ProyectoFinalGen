using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip musicClip;
    public AudioClip sfxClip;
    public Slider musicSlider;
    public Slider sfxSlider;
    public bool mute;

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
        // Cargar estado de muteo
        mute = PlayerPrefs.GetInt("Mute", 0) == 1;

        // Buscar los Sliders en la escena
        musicSlider = GameObject.Find("MusicSlider")?.GetComponent<Slider>();
        sfxSlider = GameObject.Find("sfxSlider")?.GetComponent<Slider>();

        if (musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.55f);
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
        
        if (sfxSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.55f);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        ApplyMute(); // Aplicar estado de muteo al iniciar
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

    public void PlaySFX()
    {
        if (sfxSource != null && sfxClip != null)
        {
            sfxSource.PlayOneShot(sfxClip);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = mute ? 0 : volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = mute ? 0 : volume;
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
    }

    public void ToggleMute()
    {
        mute = !mute;
        PlayerPrefs.SetInt("Mute", mute ? 1 : 0);
        ApplyMute();
    }

    private void ApplyMute()
    {
        if (musicSource != null)
        {
            musicSource.volume = mute ? 0 : PlayerPrefs.GetFloat("MusicVolume", 0.55f);
        }
        if (sfxSource != null)
        {
            sfxSource.volume = mute ? 0 : PlayerPrefs.GetFloat("SFXVolume", 0.55f);
        }
    }
}



