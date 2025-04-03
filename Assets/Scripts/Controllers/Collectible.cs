using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string nombreObjeto; // Asigna "Paper", "Kettle" o "Mug" en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador lo recoge
        {
            ObjectReveal manager = FindObjectOfType<ObjectReveal>(); 
            if (manager != null)
            {
                manager.RecogerObjeto(nombreObjeto); // Informa al script de revelación
            }
            Destroy(gameObject); // Destruye el objeto recogido
        }
    }
}