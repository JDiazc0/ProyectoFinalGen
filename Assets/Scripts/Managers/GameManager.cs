using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool juegoIniciado = false;
    //public GameObject panelVictoria;
    //public GameObject panelDerrota;
    public TextMeshProUGUI contador;

    void Start()
    {
        if (AudioManager.Instance != null)
        {
            //AudioManager.Instance.PlayMusic();
        }

        if (contador != null)
        {
            contador.gameObject.SetActive(false);

        }
        else
        {
            Debug.Log("Contador nullo");
        }
        //panelVictoria.SetActive(false);
        //panelDerrota.SetActive(false);
    }

    void Update()
    {

        if (SceneManager.GetActiveScene().name == "MainSceneCamiloDoors")
        {
            if (Input.anyKeyDown)
            {
                contador.gameObject.SetActive(true);
                Time.timeScale = 1f;
            }
        }
    }

    public void IniciarJuego()
    {


        if (contador != null)
        {
            contador.gameObject.SetActive(true);
        }
        juegoIniciado = true;
        //Time.timeScale = 0f;
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(1);
        Debug.Log("¡Has perdido!");
        if (AudioManager.Instance != null)
        {
            //AudioManager.Instance.PlaySFX();

        }

        //if (panelDerrota != null)
        //{
        //    panelDerrota.SetActive(true);
        //}
    }

    public void MostrarMensajeVictoria()
    {
        //if (panelVictoria != null)
        //{
       //     panelVictoria.SetActive(true);
       // }
        Debug.Log("¡Has ganado!");
        Time.timeScale = 0f;
        juegoIniciado = false;
    }


}
