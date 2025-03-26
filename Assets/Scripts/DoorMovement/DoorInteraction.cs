using System.Collections;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public AudioClip openSound;  // Sonido de apertura
    public AudioClip closeSound; // Sonido de cierre
    public Transform player;     // Referencia al jugador
    public float detectionRange = 2f; // Distancia para activar la puerta

    private bool isOpen = false;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Coroutine _currentCoroutine;
    private AudioSource _audioSource;

    private void Start()
    {
        _closedRotation = transform.rotation;
        _openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // Verificar la distancia entre el jugador y la puerta
        if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
                _currentCoroutine = StartCoroutine(ToggleDoor());
            }
        }
    }

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRotation = isOpen ? _closedRotation : _openRotation;

        // Reproducir sonido según el estado de la puerta
        if (isOpen && closeSound != null)
            _audioSource.PlayOneShot(closeSound);
        else if (!isOpen && openSound != null)
            _audioSource.PlayOneShot(openSound);

        isOpen = !isOpen;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
