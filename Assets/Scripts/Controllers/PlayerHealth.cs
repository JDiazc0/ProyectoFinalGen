using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 180f;
    private float currentHealth;
    public Slider healthBar;
    private bool isInToxicGas = false;
    public float damagePerSecond = 5f;
    public float regenPerSecond = 2f; // Velocidad de regeneración

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        healthBar.gameObject.SetActive(true);
    }

    void Update()
    {
        if (isInToxicGas)
        {
            TakeDamage(damagePerSecond * Time.deltaTime);
        }
        else if (currentHealth < maxHealth)
        {
            RegenerateHealth(regenPerSecond * Time.deltaTime);
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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
            GameManager gm = FindFirstObjectByType<GameManager>();
            gm.GameOver();
        }
    }

    public void RegenerateHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = currentHealth;
    }
}
