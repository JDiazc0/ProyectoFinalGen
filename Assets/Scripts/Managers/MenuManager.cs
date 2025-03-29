using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private MenuAnimation menuAnimation;

    [Header("Volume Settings")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Toggle _muteToggle;

    public static MenuManager Instance { get; private set; }

    private enum MenuState { Main, Settings, Credits }
    private MenuState currentState = MenuState.Main;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        InitializeVolumeControls();
    }

    /**
     * Musica y Efectos de sonido
     */
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

    /**
     * Menu
     */
    public void ShowMainMenu()
    {
        menuAnimation.ShowMainPanel();
        currentState = MenuState.Main;
    }

    public void ShowSettings()
    {
        menuAnimation.ShowSettingsPanel();
        currentState = MenuState.Settings;
    }

    public void ShowCredits()
    {
        menuAnimation.ShowCreditsPanel();
        currentState = MenuState.Credits;
    }

    public void CloseGame()
    {
        Application.Quit();
    }
    /* TODO: Implementar posibles opciones de:
        * - Guardar configuraciones
        * - Cargar configuraciones
        * - Restablecer configuraciones
        */
}