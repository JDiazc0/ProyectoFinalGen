using System.Collections;
using UnityEngine;

public class ElevatorControl : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 2f;
    public GameObject elevator;
    public float elevatorSpeed = 2f;
    public float elevatorHeight = -7.66f; // Ahora comienza bajando
    public AudioClip buttonSound;
    public AudioClip elevatorSound;
    public float fadeOutDuration = 1f;

    private Renderer cubeRenderer;
    private Color originalColor = Color.red;
    private bool isInRange = false;
    private bool isMoving = false;
    private bool isAtTop = true; // Ahora inicia en "arriba" y primero baja
    private AudioSource audioSource;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.color = originalColor;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        isInRange = distance < detectionRange;

        if (isInRange && Input.GetMouseButtonDown(0) && !isMoving)
        {
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
        isMoving = true;
        yield return new WaitForSeconds(0.5f);

        if (elevatorSound != null)
        {
            audioSource.clip = elevatorSound;
            audioSource.loop = true;
            audioSource.volume = 1f;
            audioSource.Play();
        }

        Vector3 startPos = elevator.transform.position;
        Vector3 targetPos = isAtTop
            ? (startPos + new Vector3(0, elevatorHeight, 0))  // Baja primero (-4.12)
            : (startPos - new Vector3(0, elevatorHeight, 0)); // Luego sube (+4.12)

        float startTime = Time.time;
        float duration = Mathf.Abs(elevatorHeight) / elevatorSpeed;

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

        elevator.transform.position = targetPos;
        isAtTop = !isAtTop; // Alternar estado

        StartCoroutine(FadeOutSound());

        cubeRenderer.material.color = originalColor;
        isMoving = false;

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

