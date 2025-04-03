using UnityEngine;

public class DoorLock : MonoBehaviour
{
    public string allowedLayer = "Office"; 
    private bool isLocked = true; 

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona NO está en la capa permitida
        if (other.gameObject.layer != LayerMask.NameToLayer(allowedLayer))
        {
            isLocked = true; 
            Debug.Log("Acceso denegado. La puerta está bloqueada.");
        }
        else
        {
            isLocked = false; 
            Debug.Log("Acceso permitido. La puerta está desbloqueada.");
        }
    }

    public bool IsDoorLocked()
    {
        return isLocked;
    }
}