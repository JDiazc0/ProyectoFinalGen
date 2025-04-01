using UnityEngine;
using UnityEngine.Video;

public class Proyector : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    private bool isNearProjector = false;
    private bool hasPlayed = false; // Bandera para saber si el video ya se reprodujo

    void Start()
    {
        videoPlayer.Stop();
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
    }

    void Update()
    {
        // Sólo reproduce si el jugador está cerca y el video no se ha reproducido ya
        if (isNearProjector && !hasPlayed)
        {
            if (!videoPlayer.isPlaying)
            {
                videoPlayer.Play();
                hasPlayed = true; // Se marca como reproducido
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearProjector = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearProjector = false;
        }
    }
}

