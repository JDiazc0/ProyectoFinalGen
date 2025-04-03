using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    private bool isRunning = false;
    
    void Start()
    {
        StartTimer();
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {elapsedTime:F2}";
        }
    }

    public void PauseTimer() => isRunning = false;
    public void ResumeTimer() => isRunning = true;

    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;        
        UpdateTimerText();
    }

    public void StopTimerAndRestartGame()
    {
        isRunning = false;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
        else
        {
            Debug.LogError("GameManager instance not found!");
        }
    }
}
