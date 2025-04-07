using System.Collections;
using UnityEngine;

public class DoubleDoorInteraction : MonoBehaviour
{
    public GameObject doorLeft;  // Asigna la puerta izquierda
    public GameObject doorRight; // Asigna la puerta derecha
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public AudioClip openSound;  // Sonido de apertura
    public AudioClip closeSound; // Sonido de cierre
    public string playerTag = "Player"; // Tag público para identificar al jugador
    public RectTransform interactionCanvas; // Referencia al Canvas

    private bool isOpen = false;
    private Quaternion closedRotationLeft, openRotationLeft;
    private Quaternion closedRotationRight, openRotationRight;
    private Coroutine _currentCoroutine;
    private AudioSource _audioSource;
    private bool _playerInRange = false;

    private void Start()
    {
        closedRotationLeft = doorLeft.transform.rotation;
        closedRotationRight = doorRight.transform.rotation;

        openRotationLeft = Quaternion.Euler(doorLeft.transform.eulerAngles - new Vector3(0, openAngle, 0));
        openRotationRight = Quaternion.Euler(doorRight.transform.eulerAngles + new Vector3(0, openAngle, 0));

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
            _currentCoroutine = StartCoroutine(ToggleDoors());
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

    private IEnumerator ToggleDoors()
    {
        Quaternion targetRotationLeft = isOpen ? closedRotationLeft : openRotationLeft;
        Quaternion targetRotationRight = isOpen ? closedRotationRight : openRotationRight;

        // Reproducir sonido de apertura o cierre
        if (isOpen && closeSound != null)
            _audioSource.PlayOneShot(closeSound);
        else if (!isOpen && openSound != null)
            _audioSource.PlayOneShot(openSound);

        isOpen = !isOpen;

        while (Quaternion.Angle(doorLeft.transform.rotation, targetRotationLeft) > 0.01f)
        {
            doorLeft.transform.rotation = Quaternion.Lerp(doorLeft.transform.rotation, targetRotationLeft, Time.deltaTime * openSpeed);
            doorRight.transform.rotation = Quaternion.Lerp(doorRight.transform.rotation, targetRotationRight, Time.deltaTime * openSpeed);
            yield return null;
        }

        doorLeft.transform.rotation = targetRotationLeft;
        doorRight.transform.rotation = targetRotationRight;
    }
}