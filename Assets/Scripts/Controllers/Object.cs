using UnityEngine;
using TMPro; 
public class Object : MonoBehaviour
{
    public GameObject objetoOculto; 
    public GameObject objetoOculto2;// Objeto que se revelará
    public TextMeshProUGUI mensajeTexto; // Texto que mostrará el mensaje
    public string mensaje = "¡Objeto encontrado,Dirígete a la sala de proyecciones!"; // Mensaje personalizado
    public float tiempoMensaje = 2f; // Tiempo que el mensaje estará visible

    private void Start()
    {
        if (objetoOculto != null)
            objetoOculto.SetActive(false); // Asegura que el objeto inicie oculto
            objetoOculto2.SetActive(false);
        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false); // Oculta el mensaje al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador colisiona
        {
            if (objetoOculto != null)
                objetoOculto.SetActive(true);
                objetoOculto2.SetActive(true); // Muestra el objeto oculto

            if (mensajeTexto != null)
            {
                mensajeTexto.text = mensaje; // Actualiza el mensaje
                mensajeTexto.gameObject.SetActive(true); // Muestra el texto
                Invoke("OcultarMensaje", tiempoMensaje); // Oculta el mensaje después de un tiempo
            }

            Destroy(gameObject); // Destruye el objeto recogido
        }
    }

    private void OcultarMensaje()
    {
        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false);
    }
}
