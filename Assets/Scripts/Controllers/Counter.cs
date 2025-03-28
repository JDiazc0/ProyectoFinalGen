using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float timeLimit = 500f;
    private float timeRemaining;
    private bool isRunning = false;

    void Start()
    {
        RestartTimer();
    }

    void Update()
    {
        if (isRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isRunning = false;
                UpdateTimerText();

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.RestartGame();
                }
                else
                {
                    Debug.LogError("GameManager instance not found!");
                }
            }
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {timeRemaining:F2}";
        }
    }

    public void PauseTimer() => isRunning = false;
    public void ResumeTimer() => isRunning = true;

    public void RestartTimer()
    {
        timeRemaining = timeLimit;
        isRunning = true;
        UpdateTimerText();
    }
}
