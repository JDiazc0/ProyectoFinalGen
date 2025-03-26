using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 120f;
    private float currentHealth;
    public Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        healthBar.gameObject.SetActive(false);        
        StartCoroutine(DamageOverTime());
    }

    IEnumerator DamageOverTime()
    {
        if (SceneManager.GetActiveScene().name == "MainSceneCamilo")
        {
            healthBar.gameObject.SetActive(true); 
            
            while (currentHealth > 0)
            {
                currentHealth -= 2f; 
                healthBar.value = currentHealth;
                yield return new WaitForSeconds(1f);
            }
            Debug.Log("Game Over");
            GameManager gm = FindFirstObjectByType<GameManager>(); 
            gm.GameOver();
            
        }
    }

    
            
                
            
        

    public void TakeDamage(float amount) // Asegúrate de que este método es público
    {
        maxHealth -= amount;
        if (maxHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto");
        GameManager gm = FindFirstObjectByType<GameManager>(); 
        gm.GameOver();;
    }
}
