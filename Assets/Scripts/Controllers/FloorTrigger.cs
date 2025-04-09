using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public int floorNumber; // 0 = Subterráneo, 1 = Primer piso, 2 = Segundo piso
    public MapManager mapManager; // Asignar manualmente en Unity

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(" Algo entró en el trigger: " + other.name);

        if (other.CompareTag("Player") || other.CompareTag("Terapia") || other.CompareTag("Exit"))
        {
            // Verifica si mapManager es null antes de usarlo
            if (mapManager != null)
            {
                Debug.Log(" Jugador detectado en el trigger del piso: " + floorNumber);
                mapManager.ChangeFloor(floorNumber); // Llamada al método ChangeFloor
            }

        }
    }
}
