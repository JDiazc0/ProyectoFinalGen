using System.Collections;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public AudioClip openSound;  // Sonido de apertura
    public AudioClip closeSound; // Sonido de cierre
    public string playerTag = "Player"; // Tag público para identificar al jugador
    public RectTransform interactionCanvas; // Referencia al Canvas

    private bool isOpen = false;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Coroutine _currentCoroutine;
    private AudioSource _audioSource;
    private bool _playerInRange = false;

    private void Start()
    {
        _closedRotation = transform.rotation;
        _openRotation = Quaternion.Euler(transform.eulerAngles - new Vector3(0, openAngle, 0));

        // Verificar y agregar AudioSource si es necesario
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            Debug.LogWarning("Se agregó un AudioSource automáticamente porque no existía.");
        }

        // Desactivar el canvas al inicio si está asignado
        if (interactionCanvas != null)
        {
            interactionCanvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(ToggleDoor());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            _playerInRange = true;
            if (interactionCanvas != null)
            {
                interactionCanvas.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            _playerInRange = false;
            if (interactionCanvas != null)
            {
                interactionCanvas.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRotation = isOpen ? _closedRotation : _openRotation;

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