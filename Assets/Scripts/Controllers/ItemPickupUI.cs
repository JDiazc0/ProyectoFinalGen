using UnityEngine;
using UnityEngine.UI;

public class ItemPickupUI : MonoBehaviour
{
    public Text pickupText;  // Referencia al texto de UI
    private GameObject objetoCercano;

    void Start()
    {
        pickupText.gameObject.SetActive(false); // Asegurar que inicie oculto
    }

    void Update()
    {
        if (objetoCercano != null)
        {
            pickupText.gameObject.SetActive(true); // Mostrar mensaje

            // Si el jugador presiona E, recoger el objeto
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpObject();
            }
        }
        else
        {
            pickupText.gameObject.SetActive(false); // Ocultar mensaje si no hay objeto cerca
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Recolectable")) // Verifica el Tag del objeto
        {
            objetoCercano = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objetoCercano)
        {
            objetoCercano = null;
        }
    }

    void PickUpObject()
    {
        Debug.Log("Recogiste: " + objetoCercano.name);
        Destroy(objetoCercano); // Destruir el objeto después de recogerlo
        objetoCercano = null; // Evitar errores
    }
}
