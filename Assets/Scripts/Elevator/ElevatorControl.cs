using System.Collections;
using UnityEngine;

public class ElevatorControl : MonoBehaviour
{
    public Transform player; // Arrastra el FirstPersonController aquí desde el Inspector
    public float detectionRange = 2f; // Distancia en metros
    public GameObject elevator; // Arrastra el elevador desde el Inspector
    public float elevatorSpeed = 2f; // Velocidad de elevación
    public float elevatorHeight = 4f; // Altura máxima del elevador
    public AudioClip buttonSound; // Sonido del botón
    public AudioClip elevatorSound; // Sonido del elevador en movimiento
    public float fadeOutDuration = 1f; // Tiempo para desvanecer el sonido

    private Renderer cubeRenderer;
    private Color originalColor = Color.red;
    private bool isInRange = false;
    private bool isMoving = false; // Bloquea el botón mientras el elevador se mueve
    private bool isAtTop = false; // Indica si el elevador está arriba o abajo
    private AudioSource audioSource;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.color = originalColor;

        // Crear un AudioSource en tiempo de ejecución
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        isInRange = distance < detectionRange;

        if (isInRange && Input.GetMouseButtonDown(0) && !isMoving) // Click izquierdo y no en movimiento
        {
            // Reproducir sonido del botón
            if (buttonSound != null)
            {
                audioSource.PlayOneShot(buttonSound);
            }

            cubeRenderer.material.color = Color.green;
            StartCoroutine(MoveElevator());
        }
    }

    IEnumerator MoveElevator()
    {
        isMoving = true; // Bloquea el botón

        // Esperar 0.5 segundos antes de iniciar el movimiento
        yield return new WaitForSeconds(0.5f);

        // Iniciar sonido del elevador si está asignado
        if (elevatorSound != null)
        {
            audioSource.clip = elevatorSound;
            audioSource.loop = true; // Sonido en bucle mientras se mueve
            audioSource.volume = 1f; // Asegurar que inicia con volumen completo
            audioSource.Play();
        }

        Vector3 startPos = elevator.transform.position;
        Vector3 targetPos = isAtTop ? (startPos - new Vector3(0, elevatorHeight, 0)) : (startPos + new Vector3(0, elevatorHeight, 0));
        float startTime = Time.time;
        float duration = elevatorHeight / elevatorSpeed;

        // Hacer que el jugador sea hijo del elevador mientras se mueve
        if (player != null)
        {
            player.SetParent(elevator.transform);
        }

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            elevator.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        elevator.transform.position = targetPos; // Asegurar posición final
        isAtTop = !isAtTop; // Alternar estado (arriba/abajo)

        // Iniciar el fade out del sonido del elevador
        StartCoroutine(FadeOutSound());

        // Restablecer el color a rojo y desbloquear el botón
        cubeRenderer.material.color = originalColor;
        isMoving = false;

        // Desvincular al jugador del elevador después de que se detenga
        if (player != null)
        {
            player.SetParent(null);
        }
    }

    IEnumerator FadeOutSound()
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeOutDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeOutDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
