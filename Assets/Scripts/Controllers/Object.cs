using UnityEngine;
using TMPro;

public class Object : MonoBehaviour
{
    public GameObject objetoOculto; // Objeto que se revelará
    public TextMeshProUGUI mensajeTexto; // Texto que mostrará el mensaje
    public string mensaje = "¡Objeto encontrado, No lo guardes o perderas tu llave a la salida!"; // Mensaje personalizado
    public float tiempoMensaje = 5f; // Tiempo que el mensaje estará visible
    public GameObject puerta;

    private void Start()
    {
        if (objetoOculto != null)
            objetoOculto.SetActive(false); // Oculta el objeto al inicio

        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false); // Oculta el mensaje al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Terapia")) return;

        // Mostrar el objeto oculto
        if (objetoOculto != null)
            objetoOculto.SetActive(true);

        // Cambiar tag del jugador si es necesario
        GameObject player = GameObject.Find("Terapia");
        if (player != null)
        {
            player.tag = "Player";
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con el nombre 'Terapia'.");
        }

        // Mostrar el mensaje temporal
        if (mensajeTexto != null)
        {
            mensajeTexto.text = mensaje;
            mensajeTexto.gameObject.SetActive(true);
            Invoke(nameof(OcultarMensaje), tiempoMensaje);
        }

        // Activar el script de la puerta
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
                Debug.LogWarning("El objeto puerta no tiene el componente DoorInteraction.");
            }
        }

        // Destruir este objeto (el trigger)
        Destroy(gameObject);
    }

    private void OcultarMensaje()
    {
        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false);
    }
}


