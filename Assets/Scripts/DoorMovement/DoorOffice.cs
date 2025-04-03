using System.Collections;
using UnityEngine;

public class DoorOffice : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public AudioClip openSound;  
    public AudioClip closeSound; 
    public float detectionRange = 1.7f;
    public string officeLayerName = "Office"; // Nombre del layer asignable

    private Transform player;
    private bool isOpen = false;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Coroutine _currentCoroutine;
    private AudioSource _audioSource;

    private void Start()
    {
        // Buscar al jugador por la etiqueta "Player"
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("No se encontró un GameObject con la etiqueta 'Player'. Asegúrate de que el jugador tenga la etiqueta correctamente asignada.");
            return;
        }

        _closedRotation = transform.rotation;
        _openRotation = Quaternion.Euler(transform.eulerAngles - new Vector3(0, openAngle, 0));

        // Verificar y agregar AudioSource si es necesario
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            Debug.LogWarning("Se agregó un AudioSource automáticamente porque no existía.");
        }
    }

    private void Update()
    {
        if (player == null) return; 

        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                if (IsTouchingOfficeObject())
                {
                    if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
                    _currentCoroutine = StartCoroutine(ToggleDoor());
                }
                else
                {
                    Debug.Log("No puedes abrir esta puerta sin interactuar con un objeto de la oficina.");
                }
            }
        }
    }

    private bool IsTouchingOfficeObject()
    {
        Collider[] colliders = Physics.OverlapSphere(player.position, 0.5f);
        int officeLayer = LayerMask.NameToLayer(officeLayerName);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.layer == officeLayer)
            {
                return true;
            }
        }
        return false;
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
