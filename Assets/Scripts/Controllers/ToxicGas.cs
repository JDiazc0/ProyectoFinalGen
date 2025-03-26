using UnityEngine;

public class ToxicGas : MonoBehaviour
{
    public float damageRate = 5f; 

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damageRate * Time.deltaTime);
        }
    }
}