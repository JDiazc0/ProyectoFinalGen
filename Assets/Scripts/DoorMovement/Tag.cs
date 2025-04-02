using UnityEngine;

public class Tag : MonoBehaviour
{
    
    public string targetLayerName = "Office"; // Nombre del layer a asignar
    public float interactionRange = 2f; // Rango de interacción
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("No se encontró un GameObject con la etiqueta 'Player'.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        if (Vector3.Distance(transform.position, player.position) <= interactionRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AssignLayerToPlayer();
            }
        }
    }

    private void AssignLayerToPlayer()
    {
        int targetLayer = LayerMask.NameToLayer(targetLayerName);
        if (targetLayer != -1)
        {
            player.gameObject.layer = targetLayer;
            Debug.Log("El jugador ahora tiene el layer: " + targetLayerName);
        }
        else
        {
            Debug.LogError("El layer especificado no existe. Verifica el nombre en el Inspector.");
        }
    }
}
