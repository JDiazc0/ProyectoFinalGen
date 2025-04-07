using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NpcWallCollitionSound : MonoBehaviour
{
    public AudioClip soundClip;
    public float minTime = 3f; // tiempo mínimo entre sonidos
    public float maxTime = 3f; // tiempo máximo entre sonidos

    private AudioSource audioSource;
    private float nextPlayTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f; // Sonido completamente 3D
        audioSource.playOnAwake = false;

        ScheduleNextSound();
    }

    void Update()
    {
        if (Time.time >= nextPlayTime)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
            ScheduleNextSound();
        }
    }

    void ScheduleNextSound()
    {
        // Programa el próximo sonido en un intervalo aleatorio
        nextPlayTime = Time.time + Random.Range(minTime, maxTime);
    }
}
