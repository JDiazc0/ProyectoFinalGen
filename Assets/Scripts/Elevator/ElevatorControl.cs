using System.Collections;
using UnityEngine;

public class ElevatorControl : MonoBehaviour
{
    public Transform player; // Arrastra el FirstPersonController aqu� desde el Inspector
    public float detectionRange = 2f; // Distancia en metros
    public GameObject elevator; // Arrastra el elevador desde el Inspector
    public float elevatorSpeed = 2f; // Velocidad de elevaci�n
    public float elevatorHeight = 4f; // Altura m�xima del elevador
    public AudioClip buttonSound; // Sonido del bot�n
    public AudioClip elevatorSound; // Sonido del elevador en movimiento
    public float fadeOutDuration = 1f; // Tiempo para desvanecer el sonido

    private Renderer cubeRenderer;
    private Color originalColor = Color.red;
    private bool isInRange = false;
    private bool isMoving = false; // Bloquea el bot�n mientras el elevador se mueve
    private bool isAtTop = false; // Indica si el elevador est� arriba o abajo
    private AudioSource audioSource;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.color = originalColor;

        // Crear un AudioSource en tiempo de ejecuci�n
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        isInRange = distance < detectionRange;

        if (isInRange && Input.GetMouseButtonDown(0) && !isMoving) // Click izquierdo y no en movimiento
        {
            // Reproducir sonido del bot�n
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
        isMoving = true; // Bloquea el bot�n

        // Esperar 0.5 segundos antes de iniciar el movimiento
        yield return new WaitForSeconds(0.5f);

        // Iniciar sonido del elevador si est� asignado
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

        elevator.transform.position = targetPos; // Asegurar posici�n final
        isAtTop = !isAtTop; // Alternar estado (arriba/abajo)

        // Iniciar el fade out del sonido del elevador
        StartCoroutine(FadeOutSound());

        // Restablecer el color a rojo y desbloquear el bot�n
        cubeRenderer.material.color = originalColor;
        isMoving = false;

        // Desvincular al jugador del elevador despu�s de que se detenga
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
