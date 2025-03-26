using UnityEngine;
using UnityEngine.Video;

public class Proyector : MonoBehaviour
{
   
    public VideoPlayer videoPlayer;
    public AudioSource audioSource; 
    public GameObject messageUI; // Mensaje de "Presiona E para activar"
    private bool isNearProjector = false;

    void Start()
    {
        messageUI.SetActive(false); // Ocultar mensaje al inicio
        videoPlayer.Stop();
    }

    void Update()
    {
        if (isNearProjector && Input.GetKeyDown(KeyCode.E))
        {
            if (!videoPlayer.isPlaying)
            {
                videoPlayer.Play();
                audioSource.Play(); // 🔊 Reproducir el sonido del video
                messageUI.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearProjector = true;
            messageUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearProjector = false;
            messageUI.SetActive(false);
        }
    }
}
