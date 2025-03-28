using UnityEngine;

public class Keys : MonoBehaviour
{
 public Transform[] spawnPoints; 
    public float fixedY = 0.0f; 
    public float pickupRange = 1.0f; 
    public string officeLayerName = "Office";
    public bool isNearPlayer = false; 

    void Start()
    {
        if (spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length); 
            Vector3 newPosition = spawnPoints[randomIndex].position; 
            newPosition.y = fixedY; 
            transform.position = newPosition; 
        }
        else
        {
            Debug.LogWarning("No hay puntos de aparición asignados en el Inspector.");
        }
    }

    void Update()
    {
        if (isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            PickUpKey();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
            Debug.Log("Presiona 'E' para recoger la llave");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }

    void PickUpKey()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 
        if (player != null)
        {
            int officeLayer = LayerMask.NameToLayer(officeLayerName); 
            if (officeLayer != -1) 
            {
                player.layer = officeLayer; 
                Debug.Log("El jugador ahora tiene el Layer: " + officeLayerName);
            }
            else
            {
                Debug.LogWarning("El Layer 'Office' no existe. Verifica en el Inspector de Unity.");
            }
        }

        Debug.Log("Llave recogida");
        Destroy(gameObject); // Destruye la llave después de recogerla
    }
}