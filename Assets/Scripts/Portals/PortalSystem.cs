using System.Collections;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    private Transform player;        // Referencia dinámica al jugador
    public Transform targetSpawn;    // Punto de destino del portal
    public float detectionRange = 1f; // Distancia para activar el portal
    public AudioClip portalSound;    // Sonido del portal (opcional)

    private AudioSource audioSource;
    private bool isTeleporting = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("No se encontró un GameObject con la etiqueta 'Player'.");
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (player == null) return; // Evita errores si no hay jugador asignado

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRange && !isTeleporting)
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    IEnumerator TeleportPlayer()
    {
        isTeleporting = true;

        if (portalSound != null)
        {
            audioSource.PlayOneShot(portalSound);
        }

        yield return new WaitForSeconds(0.1f);

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        player.position = new Vector3(targetSpawn.position.x, player.position.y, targetSpawn.position.z);

        yield return new WaitForSeconds(0.1f);

        if (controller != null)
        {
            controller.enabled = true;
        }

        isTeleporting = false;
    }
}
