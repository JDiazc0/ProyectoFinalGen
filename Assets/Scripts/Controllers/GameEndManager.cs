using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    public TextMeshProUGUI messageText; 
    public float gameEndDelay = 3f; 

    public void ShowVictory()
    {
        ShowMessage("¡Has ganado!", Color.green);
    }

    public void ShowDefeat()
    {
        ShowMessage("Game Over", Color.red);
    }

    private void ShowMessage(string message, Color color)
    {
        if (messageText != null)
        {
            messageText.text = message;
            messageText.color = color;
            messageText.gameObject.SetActive(true);
        }
        Invoke(nameof(RestartGame), gameEndDelay);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
