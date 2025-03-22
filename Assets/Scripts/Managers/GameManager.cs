using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool juegoIniciado = false;
    public int puntaje;
    public GameObject panelVictoria;
    public GameObject inicialText;
   

    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic();
        }
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            IniciarJuego();
            if (inicialText != null)
            {
                inicialText.SetActive(false);
            }
        }
    }

    public void IniciarJuego()
    {
        juegoIniciado = true;
        Time.timeScale = 1f;
        if (inicialText != null)
        {
            inicialText.SetActive(true);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(1);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("SFX");
        }
    }

    public void MostrarMensajeVictoria()
    {
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
        }
        Debug.Log("¡Has ganado!");
        Time.timeScale = 0f;
        juegoIniciado = false;
    }
    
}
