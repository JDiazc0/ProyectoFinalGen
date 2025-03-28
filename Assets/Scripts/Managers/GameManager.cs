using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        Debug.Log("Juego Iniciado");
        SceneManager.LoadScene(1);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        Debug.Log("¡Has perdido!");
    }

    public void GameWon()
    {
        Debug.Log("¡Has ganado!");
    }


    public bool TogglePause()
    {
        isPaused = !isPaused;

        Debug.Log("toggle");
        if (isPaused)
        {
            Debug.Log("Pausa");
            Time.timeScale = 0f;
        }
        else
        {
            Debug.Log("Despausa");
            Time.timeScale = 1f;
        }

        return isPaused;
    }

    public void ResumeGame()
    {
        TogglePause();
    }

    public void ReturnToMainMenu()
    {
        TogglePause();
        Debug.Log("Regresando al menú principal...");
        SceneManager.LoadScene(0);
    }

}
