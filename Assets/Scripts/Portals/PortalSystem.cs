using System.Collections;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    public Transform player;         // Referencia al jugador
    public Transform targetSpawn;    // Punto de destino del portal
    public float detectionRange = 1f; // Distancia para activar el portal
    public AudioClip portalSound;    // Sonido del portal (opcional)


    private AudioSource audioSource;
    private bool isTeleporting = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
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

        yield return new WaitForSeconds(0.1f); // Breve pausa antes de mover

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false; // Desactivar para evitar colisión
        }

        // Teletransportar manteniendo la altura original del jugador
        player.position = new Vector3(targetSpawn.position.x, player.position.y, targetSpawn.position.z);

        yield return new WaitForSeconds(0.1f); // Espera corta antes de reactivar el control

        if (controller != null)
        {
            controller.enabled = true; // Reactivar el movimiento
        }

        isTeleporting = false;
    }
}
