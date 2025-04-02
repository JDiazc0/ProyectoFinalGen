using UnityEngine;
using TMPro;
using System.Collections;

public class Puzzle: MonoBehaviour
{
    public GameObject puerta; // La puerta que se abrirá
    public GameObject[] objetosRequeridos; // Lista de los 3 objetos
    public Transform[] posicionesObjetivo; // Lista de las posiciones correctas   
    public TextMeshProUGUI mensajeUI; // Texto en pantalla para mostrar "Posición correcta"

    private int objetosColocados = 0; // Contador de objetos en su lugar
    private bool[] objetoCorrecto; // Array para saber qué objetos ya fueron colocados correctamente

    void Start()
    {
        puerta.SetActive(false); // La puerta empieza cerrada
        mensajeUI.gameObject.SetActive(false); // Ocultar el mensaje al inicio
        objetoCorrecto = new bool[objetosRequeridos.Length]; // Inicializa el array de objetos colocados
    }

    void Update()
    {
        // Verifica cuántos objetos están correctamente colocados
        objetosColocados = 0;

        for (int i = 0; i < objetosRequeridos.Length; i++)
        {
            if (!objetoCorrecto[i]) // Si el objeto aún no se ha colocado correctamente
            {
                if (Vector3.Distance(objetosRequeridos[i].transform.position, posicionesObjetivo[i].position) < 0.5f)
                {
                    // Ajustar la posición del objeto a la correcta
                    objetosRequeridos[i].transform.position = posicionesObjetivo[i].position;
                    objetosRequeridos[i].transform.rotation = posicionesObjetivo[i].rotation; // Alinea también la rotación
                    
                    objetoCorrecto[i] = true; // Marca el objeto como colocado correctamente
                    objetosColocados++; // Aumenta el contador

                    // Muestra el mensaje en pantalla
                    StartCoroutine(MostrarMensaje("¡Posición correcta!"));
                }
            }
            else
            {
                objetosColocados++;
            }
        }

        // Si los 3 objetos están en su sitio, activa la puerta
        if (objetosColocados == objetosRequeridos.Length)
        {
            AbrirPuerta();
        }
    }

    void AbrirPuerta()
    {
        puerta.SetActive(true);
        Debug.Log("¡Todos los objetos están en su lugar! Puerta abierta.");
    }

    IEnumerator MostrarMensaje(string mensaje)
    {
        mensajeUI.text = mensaje;
        mensajeUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f); // Muestra el mensaje por 2 segundos
        mensajeUI.gameObject.SetActive(false);
    }
}
