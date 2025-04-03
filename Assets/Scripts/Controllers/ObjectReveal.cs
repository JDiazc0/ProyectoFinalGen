using UnityEngine;
using System.Collections.Generic;

public class ObjectReveal : MonoBehaviour
{
    public GameObject objetoOculto; // Objeto que se revelará
    private HashSet<string> objetosRecolectados = new HashSet<string>(); // Almacena los nombres de los objetos recogidos

    private void Start()
    {
        if (objetoOculto != null)
        {
            objetoOculto.SetActive(false); // Oculta el objeto al inicio
        }
    }

    public void RecogerObjeto(string nombreObjeto)
    {
        // Verifica si el objeto es uno de los requeridos
        if (nombreObjeto == "Paper" || nombreObjeto == "Kettle" || nombreObjeto == "Mug")
        {
            objetosRecolectados.Add(nombreObjeto); // Agrega a la lista si es válido
        }

        // Si ya tiene los tres objetos necesarios, revela el objeto oculto
        if (objetosRecolectados.Count == 3 && objetoOculto != null)
        {
            objetoOculto.SetActive(true);
        }
    }
}

