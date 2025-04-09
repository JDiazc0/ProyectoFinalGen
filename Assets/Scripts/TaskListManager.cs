using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskListManager : MonoBehaviour
{
    [Header("Referencias")]
    public TextMeshProUGUI taskDisplay;

    private List<string> tasks = new List<string>();
    private Coroutine mostrarCoroutine;
    public float tiempoVisible = 5f; // segundos que el mensaje permanece en pantalla

    void Start()
    {
        tasks.Add("Reproducir primera claqueta de cine");
        tasks.Add("Ir a la oficina del director");
        tasks.Add("Reproducir segunda claqueta de cine");
        tasks.Add("Ir a la sala terapia");
        tasks.Add("Encontrar el primer objeto clave");
        tasks.Add("Encontrar salida del bucle de portales");
        tasks.Add("Reproducir tercera claqueta de cine");
        tasks.Add("Encontrar el segundo objeto clave");
        tasks.Add("Encontrar la rata con el gen secreto");
        tasks.Add("Abrir la puerta de salida del asilo"); 

        MostrarTareasPorTiempo();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            MostrarTareasPorTiempo(); // Muestra las tareas al presionar Z
        }
    }

    public void CompletarTarea(int index)
    {
        if (index >= 0 && index < tasks.Count)
        {
            tasks.RemoveAt(index);
            MostrarTareasPorTiempo();
        }
        else
        {
            Debug.LogWarning("Índice fuera de rango al intentar completar tarea.");
        }
    }

    public void CompletarTareaPorTexto(string texto)
{
    int index = tasks.FindIndex(t => t == texto);
    if (index != -1)
    {
        CompletarTarea(index);
    }
    else
    {
        Debug.LogWarning("No se encontró una tarea con ese texto.");
    }
}

    public void AgregarTarea(string nuevaTarea)
    {
        tasks.Add(nuevaTarea);
        MostrarTareasPorTiempo();
    }

    private void MostrarTareasPorTiempo()
    {
        if (mostrarCoroutine != null)
            StopCoroutine(mostrarCoroutine);

        mostrarCoroutine = StartCoroutine(MostrarTemporalmente());
    }

    private IEnumerator MostrarTemporalmente()
    {
        taskDisplay.text = "Tareas pendientes:\n";

        foreach (var tarea in tasks)
        {
            taskDisplay.text += $"• {tarea}\n";
        }

        if (tasks.Count == 0)
            taskDisplay.text = "¡Todas las tareas están completas!";

        taskDisplay.gameObject.SetActive(true);

        yield return new WaitForSeconds(tiempoVisible);

        taskDisplay.gameObject.SetActive(false);
    }
}
