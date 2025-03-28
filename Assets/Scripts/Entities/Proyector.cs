using UnityEngine;
using UnityEngine.Video;

using UnityEngine;
using UnityEngine.Video;

public class Proyector : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;     
    private bool isNearProjector = false;

    void Start()
    {
        
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
