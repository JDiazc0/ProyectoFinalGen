using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{public bool juegoIniciado = false;
    public int puntaje;
    public GameObject panelVictoria;
    public GameObject inicialText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       //AudioManager.Instance.PlayMusic("Game Theme"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            IniciarJuego();
            inicialText.SetActive(false);
        }
        
    }

    public void IniciarJuego()
    {
        juegoIniciado = true;
        Time.timeScale = 1f;        
        inicialText.SetActive(true);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(1);
        //AudioManager.Instance.PlayMusic("Game Over Theme");
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
