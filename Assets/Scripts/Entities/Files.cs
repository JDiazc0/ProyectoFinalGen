using UnityEngine;
using TMPro;

public class Files : MonoBehaviour
{
    public TextMeshProUGUI expedienteTexto; // Referencia al TextMeshProUGUI en la UI
    public string expediente = "Daniel Carte\nEdad: 23 años\nDiagnóstico: Trastorno de Ansiedad Generalizada (TAG), Episodio Depresivo Moderado.\nMotivo de Consulta: Ansiedad intensa, insomnio y dificultad para concentrarse.\nTratamiento: Psicoterapia en la Sala de Terapia, Escitalopram 10 mg/día, técnicas de relajación y seguimiento en dos semanas.";
    private bool jugadorCerca = false;

    void Start()
    {
        expedienteTexto.gameObject.SetActive(false); // Oculta el texto al inicio
    }

    void Update()
    {
        if (jugadorCerca ) 
        {
            expedienteTexto.text = expediente;
            expedienteTexto.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tiene el tag "Player"
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            expedienteTexto.gameObject.SetActive(false); // Oculta el texto al salir
        }
    }
}
