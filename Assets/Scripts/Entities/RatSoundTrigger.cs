using UnityEngine;

public class RatSoundTrigger : MonoBehaviour
{
    public AudioSource ratAudio;
    public float soundInterval = 3f; // segundos entre chillidos
    private bool playerNearby = false;
    private float timer = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            timer = soundInterval; // para que suene al instante
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            timer = 0f;
        }
    }

    void Update()
    {
        if (playerNearby)
        {
            timer += Time.deltaTime;
            if (timer >= soundInterval)
            {
                ratAudio.Play();
                timer = 0f;
            }
        }
    }
}
