using UnityEngine;
using UnityEngine.Video;

public class Proyector : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    private bool isNearProjector = false;
    private bool hasPlayed = false;
    public string grabbableLayerName = "Grabbable"; 

    void Start()
    {
        videoPlayer.Stop();
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
    }

    void Update()
    {
        if (isNearProjector && !hasPlayed)
        {
            if (!videoPlayer.isPlaying)
            {
                videoPlayer.Play();
                hasPlayed = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(grabbableLayerName))
        {
            isNearProjector = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(grabbableLayerName))
        {
            isNearProjector = false;
            videoPlayer.Stop(); // Opcional: Detener el video cuando el objeto se aleje
            hasPlayed = false; // Permite volver a reproducir el video si el objeto se acerca de nuevo
        }
    }
}


