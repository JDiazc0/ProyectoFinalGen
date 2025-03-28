using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject pausePanel;
    public Slider _masterSlider;
    public Slider _musicSlider;
    public Slider _sfxSlider;
    public Toggle _muteToggle;

    private void Start()
    {
        InitializeVolumeControls();
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        bool isPaused = GameManager.Instance.TogglePause();
        pausePanel.SetActive(isPaused);
    }

    private void InitializeVolumeControls()
    {
        float masterVolume = PlayerPrefs.GetFloat("Master", -5f);
        float musicVolume = PlayerPrefs.GetFloat("Music", -5f);
        float sfxVolume = PlayerPrefs.GetFloat("SFX", -5f);
        bool isMuted = PlayerPrefs.GetFloat("Master", 0f) <= -40f;

        _masterSlider.value = masterVolume;
        _musicSlider.value = musicVolume;
        _sfxSlider.value = sfxVolume;
        _muteToggle.isOn = isMuted;

        AudioManager.Instance.SetMasterVolume(masterVolume);
        AudioManager.Instance.SetMusicVolume(musicVolume);
        AudioManager.Instance.SetSFXVolume(sfxVolume);
        if (isMuted) AudioManager.Instance.ToggleMute();

        _masterSlider.onValueChanged.AddListener(ChangeMasterVolume);
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        _sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
        _muteToggle.onValueChanged.AddListener(ToggleMute);
    }

    public void ChangeMasterVolume(float volume)
    {
        AudioManager.Instance.SetMasterVolume(volume);
        _muteToggle.isOn = volume <= -40f;
    }

    public void ChangeMusicVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
    }

    public void ToggleMute(bool isMuted)
    {
        AudioManager.Instance.ToggleMute();
        if (isMuted)
        {
            _masterSlider.value = -40f;
            AudioManager.Instance.SetMasterVolume(-40f);
        }
        else
        {
            _masterSlider.value = 0f;
            AudioManager.Instance.SetMasterVolume(0f);
        }
    }

    public void ResumeGame()
    {
        TogglePause();
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.ReturnToMainMenu();
    }
}
