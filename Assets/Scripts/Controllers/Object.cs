using UnityEngine;
using TMPro; 
public class Object : MonoBehaviour
{
    public GameObject objetoOculto; // Objeto que se revelará
    
    public TextMeshProUGUI mensajeTexto; // Texto que mostrará el mensaje
    public string mensaje = "¡Objeto encontrado,Dirígete a la cocina!"; // Mensaje personalizado
    public float tiempoMensaje = 2f; // Tiempo que el mensaje estará visible

    public GameObject puerta;

    private void Start()
    {
        if (objetoOculto != null)
            objetoOculto.SetActive(false); // Asegura que el objeto inicie oculto
            // Asegura que el objeto inicie oculto

        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false); // Oculta el mensaje al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terapia")) // Verifica si el jugador colisiona
        {
            if (objetoOculto != null)
                objetoOculto.SetActive(true); // Muestra el objeto oculto
                // Muestra el objeto oculto

            GameObject player = GameObject.Find("Terapia");
            if (player != null)
            {
                player.tag = "Player";
            }
            else
            {
                Debug.LogWarning("No se encontró un objeto con el nombre 'Player'.");
            }

            if (mensajeTexto != null)
            {
                mensajeTexto.text = mensaje; // Actualiza el mensaje
                mensajeTexto.gameObject.SetActive(true); // Muestra el texto
                Invoke("OcultarMensaje", tiempoMensaje); // Oculta el mensaje después de un tiempo
            }

            Destroy(gameObject); // Destruye el objeto recogido
        }

        if (other.CompareTag("Terapia"))
        {
            if (puerta != null)
            {
                DoorInteraction doorScript = puerta.GetComponent<DoorInteraction>();
                if (doorScript != null)
                {
                    doorScript.enabled = true;
                    Debug.Log("¡Script de la puerta activado!");
                }
                else
                {
                    Debug.LogWarning("El objeto no tiene el componente DoorInteraction.");
                }
            }
        }
    }

    private void OcultarMensaje()
    {
        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false);
    }
}
