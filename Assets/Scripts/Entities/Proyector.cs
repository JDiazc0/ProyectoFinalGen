using UnityEngine;
using UnityEngine.Video;

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

        // Configurar el VideoPlayer para usar su propio audio
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
    }

    void Update()
    {
        if (isNearProjector)
        {
            if (!videoPlayer.isPlaying)
            {
                videoPlayer.Play();
                messageUI.SetActive(true);
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
