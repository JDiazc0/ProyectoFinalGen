using UnityEngine;
using TMPro;

public class MutantRatTrigger : MonoBehaviour
{
    public TextMeshProUGUI mensajeTexto;
    public string mensaje = "Has encontrado la rata con el gen mutante. Esta será prueba suficiente para revelar los secretos del doctor.";
    public float tiempoMensaje = 7f;

    private void Start()
    {
        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false); // Oculta el mensaje al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Terapia")) return; // Solo responde si el objeto tiene el tag "Terapia"

        // Mostrar mensaje
        if (mensajeTexto != null)
        {
            mensajeTexto.text = mensaje;
            mensajeTexto.gameObject.SetActive(true);
            Invoke(nameof(OcultarMensaje), tiempoMensaje);
            other.tag = "Exit";
        }

        // Cambiar la etiqueta del objeto colisionado
        Debug.Log($"La etiqueta de {other.name} ahora es: {other.tag}");
    }

    private void OcultarMensaje()
    {
        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false);
    }
}
