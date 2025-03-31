using UnityEngine;

public class ToxicGas : MonoBehaviour
{
    public float damagePerSecond = 5f;

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
}