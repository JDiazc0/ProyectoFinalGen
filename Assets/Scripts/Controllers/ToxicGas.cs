using UnityEngine;

public class ToxicGas : MonoBehaviour
{
    public float damagePerSecond = 5f;

    [Header("Visual Settings")]
    public Color gasColor; // Paleta editable

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Visualizar el área del gas con el color en el Editor
        Gizmos.color = gasColor;
        Gizmos.DrawWireSphere(transform.position, 1f); // Ejemplo de gizmo
    }
}
