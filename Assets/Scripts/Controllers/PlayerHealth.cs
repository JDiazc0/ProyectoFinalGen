using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 120f;
    private float currentHealth;
    public Slider healthBar;
    private bool isInToxicGas = false;
    public float damagePerSecond = 5f;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        healthBar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isInToxicGas)
        {
            TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToxicGas"))
        {
            isInToxicGas = true;
            healthBar.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ToxicGas"))
        {
            isInToxicGas = false;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
            GameManager gm = FindFirstObjectByType<GameManager>();
            gm.GameOver();
        }
    }
}
