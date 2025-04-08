using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FinalManager : MonoBehaviour
{
    [Header("Configuración")]
    public string interactableTag = "Player";
    public AudioClip audioClip;
    public float sequenceDuration = 5f;
    public GameObject imagePanel;
    public Image targetImage;
    public GameObject interactionCanvas;

    [Header("Botones")]
    public GameObject endButtons;
    public Button restartButton;
    public Button quitButton;
    private FirstPersonLook lookScript;


    [Header("Arrays de Imágenes")]
    public Sprite[] imageFinalOne;
    public Sprite[] imageFinalTwo;
    public Sprite[] imageFinalThree;

    [Header("Umbrales de Tiempo")]
    public float bestTimeThreshold = 600f;
    public float mediumTimeThreshold = 300f;

    private bool _playerInRange = false;
    private bool _isPlayingSequence = false;
    private Counter _timer;

    private void Start()
    {
        _timer = Object.FindFirstObjectByType<Counter>();
        lookScript = Object.FindFirstObjectByType<FirstPersonLook>();
        if (imagePanel != null)
        {
            imagePanel.SetActive(false);
        }
        if (endButtons != null)
        {
            endButtons.SetActive(false);
        }
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartGame);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitGame);
        }
    }

    private void Update()
    {
        if (_playerInRange && Input.GetKeyDown(KeyCode.E) && !_isPlayingSequence)
        {
            StartCoroutine(PlayImageSequence());
        }
    }

    private IEnumerator PlayImageSequence()
    {
        _isPlayingSequence = true;
        GameManager.Instance.TogglePause();
        if (lookScript != null)
        {
            lookScript.UpdateCursorState();
        }
        if (audioClip != null)
        {
            AudioManager.Instance.PlayMusic(audioClip, true);
        }

        if (imagePanel != null)
        {
            imagePanel.SetActive(true);
        }

        Sprite[] selectedImages = GetImageArrayBasedOnTime();
        float timePerImage = sequenceDuration / Mathf.Max(1, selectedImages.Length);

        foreach (var image in selectedImages)
        {
            if (targetImage != null && image != null)
            {
                targetImage.sprite = image; // Usamos sprite en lugar de texture
            }
            yield return new WaitForSecondsRealtime(timePerImage);
        }

        if (endButtons != null)
        {
            endButtons.SetActive(true);
        }

    }

    private Sprite[] GetImageArrayBasedOnTime()
    {
        if (_timer == null) return imageFinalThree;

        float remainingTime = _timer.GetRemainingTimeInSeconds();

        if (remainingTime >= bestTimeThreshold)
        {
            return imageFinalOne;
        }
        else if (remainingTime >= mediumTimeThreshold)
        {
            return imageFinalTwo;
        }
        else
        {
            return imageFinalThree;
        }
    }

    private void OnRestartGame()
    {
        if (GameManager.Instance.IsPaused)
        {
            GameManager.Instance.TogglePause();
        }
        if (lookScript != null)
        {
            lookScript.UpdateCursorState();
        }
        GameManager.Instance.RestartGame();
    }

    private void OnQuitGame()
    {
        GameManager.Instance.ReturnToMainMenu();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(interactableTag))
        {
            _playerInRange = true;
            if (interactionCanvas != null)
            {
                interactionCanvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(interactableTag))
        {
            _playerInRange = false;
            if (interactionCanvas != null)
            {
                interactionCanvas.SetActive(false);
            }
        }
    }
}