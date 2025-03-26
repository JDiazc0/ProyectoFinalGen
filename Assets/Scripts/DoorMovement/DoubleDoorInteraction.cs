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
    public Transform player;     // Referencia al jugador
    public float detectionRange = 2.5f; // Distancia para activar la puerta

    private bool isOpen = false;
    private Quaternion closedRotationLeft, openRotationLeft;
    private Quaternion closedRotationRight, openRotationRight;
    private Coroutine _currentCoroutine;
    private AudioSource _audioSource;

    private void Start()
    {
        closedRotationLeft = doorLeft.transform.rotation;
        closedRotationRight = doorRight.transform.rotation;

        openRotationLeft = Quaternion.Euler(doorLeft.transform.eulerAngles - new Vector3(0, openAngle, 0));
        openRotationRight = Quaternion.Euler(doorRight.transform.eulerAngles + new Vector3(0, openAngle, 0));

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // Verificar si el jugador está cerca
        if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
                _currentCoroutine = StartCoroutine(ToggleDoors());
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
