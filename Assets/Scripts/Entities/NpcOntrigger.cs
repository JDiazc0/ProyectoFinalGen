using UnityEngine;

public class NPCVoiceTrigger : MonoBehaviour
{
    private AudioSource npcAudio;
    private bool hasPlayed = false;

    void Start()
    {
        npcAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !npcAudio.isPlaying && !hasPlayed)
        {
            npcAudio.Play();
            hasPlayed = true;
        }
    }
}


