using UnityEngine;
using TMPro; 
public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contadorTexto; 
    [SerializeField] private float tiempoLimite = 60f; 
    private float tiempoRestante;
    private bool contando = true; 

    void Start()
    {
        tiempoRestante = tiempoLimite; 
        ActualizarTexto();
    }

    void Update()
    {
        if (contando && tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante < 0) tiempoRestante = 0; 
            ActualizarTexto();
        }
    }

    private void ActualizarTexto()
    {
        if (contadorTexto != null)
        {
            contadorTexto.text = "Tiempo: " + tiempoRestante.ToString("F2") ; 
        }
    }
  
    public void PausarContador() => contando = false;
    public void ReanudarContador() => contando = true;
    public void ReiniciarContador()
    {
        tiempoRestante = tiempoLimite;
        contando = true;
        ActualizarTexto();
    }
}
