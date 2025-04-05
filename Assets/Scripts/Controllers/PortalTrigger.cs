using UnityEngine;
using TMPro; // Importa TextMeshPro

public class PortalTrigger : MonoBehaviour
{
    public TextMeshProUGUI mensajeUI; // Referencia al TextMeshPro en la UI

    void Start()
    {
        mensajeUI.gameObject.SetActive(false); // Ocultar mensaje al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador entra en el portal
        {
            MostrarMensaje();
        }
    }

    void MostrarMensaje()
    {
        mensajeUI.text = "Soy un objeto de cocina,\nte ayudo en cada sorbo.\nTe acompaño en mañanas frías,\ncon café, té o algo rojo.\n\n¿Qué soy?";
        mensajeUI.gameObject.SetActive(true); // Muestra el mensaje
        Invoke("OcultarMensaje", 5f); // Oculta el mensaje después de 5 segundos
    }

    void OcultarMensaje()
    {
        mensajeUI.gameObject.SetActive(false);
    }
}