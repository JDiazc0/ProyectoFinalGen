using UnityEngine;

public class FloorTrigger1 : MonoBehaviour
{
    public int floorNumber; // 0 = Subterr�neo, 1 = Primer piso, 2 = Segundo piso
    public MapManager mapManager; // Asignar manualmente en Unity

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(" Algo entro en el trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            // Verifica si mapManager es null antes de usarlo
            if (mapManager != null)
            {
                Debug.Log(" Jugador detectado en el trigger del piso: " + floorNumber);
                mapManager.ChangeFloor(floorNumber); // Llamada al m�todo ChangeFloor
            }

        }
    }
}
