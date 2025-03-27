using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;
public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contadorTexto; 
    private float tiempoLimite = 120f; 
    private float tiempoRestante;
    public bool contando = false; 
    private GameManager gameManager;

    void Start()
    {
         gameManager = Object.FindFirstObjectByType<GameManager>();
        if(contando){
        tiempoRestante = tiempoLimite; 
        ActualizarTexto();
        }
    }

    void Update()
    {
        if (contando && tiempoRestante > 0 && SceneManager.GetActiveScene().name == "MainSceneA")
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante < 0) 
            {
                tiempoRestante = 0; 
                ActualizarTexto();
                
                // Llamar a GameOver cuando el tiempo llegue a 0
                if (gameManager != null)
                {
                    gameManager.GameOver();
                }
                else
                {
                    Debug.LogError("GameManager no encontrado en la escena.");
                }
            }
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
