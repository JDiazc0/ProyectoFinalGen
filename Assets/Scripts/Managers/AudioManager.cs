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
 

            musicSource.volume = volume;
 

            PlayerPrefs.SetFloat("MusicVolume", volume);
 

        }
 

    }
 


 

    public void SetSFXVolume(float volume)
 

    {
 

        if (sfxSource != null)
 

        {
 

            sfxSource.volume = volume;
 

            PlayerPrefs.SetFloat("SFXVolume", volume);
 

        }
 

    }
 

}


