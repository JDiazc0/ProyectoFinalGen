// MenuManager.cs (en la carpeta Managers)
using UnityEngine;
using TMPro;
public class MenuManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private MenuAnimation menuAnimation;

    [Header("Configuración")]


    public static MenuManager Instance { get; private set; }

    private enum MenuState { Main, Settings, Credits }
    private MenuState currentState = MenuState.Main;
     

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
         
    }

    public void ShowMainMenu()
    {
        menuAnimation.ShowMainPanel();
        currentState = MenuState.Main;

    }

    public void ShowSettings()
    {
        menuAnimation.ShowSettingsPanel();
        currentState = MenuState.Settings;
    }
    /*
    public void ShowCredits()
    {
        menuAnimation.ShowCreditsPanel();
        currentState = MenuState.Credits;
        
    }*/

    /* TODO: Implementar posibles opciones de:
        * - Guardar configuraciones
        * - Cargar configuraciones
        * - Restablecer configuraciones
        */
}