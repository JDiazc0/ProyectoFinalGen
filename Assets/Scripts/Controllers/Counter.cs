using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float initialTime = 900f;

    private float remainingTime;
    private bool isRunning = false;

    void Start()
    {
        StartTimer();
    }

    void Update()
    {
        if (isRunning)
        {
            remainingTime -= Time.deltaTime;
            remainingTime = Mathf.Max(0f, remainingTime);
            UpdateTimerText();

            if (remainingTime <= 0f)
            {
                StopTimerAndRestartGame();
            }
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            // Formato minutos:segundos (MM:SS)
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);
            timerText.text = $"Tiempo Restante: \n{minutes:00}:{seconds:00}";
        }
    }

    public void PauseTimer() => isRunning = false;
    public void ResumeTimer() => isRunning = true;

    public void StartTimer()
    {
        remainingTime = initialTime;
        isRunning = true;
        UpdateTimerText();
    }

    public float GetRemainingTimeInSeconds() => remainingTime;

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