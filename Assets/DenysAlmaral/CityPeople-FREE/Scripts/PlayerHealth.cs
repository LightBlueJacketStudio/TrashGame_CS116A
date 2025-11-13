using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;
    
    [Header("UI")]
    public TextMesh healthText;
    public GameObject gameOverText;  // NEW!
    
    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
        
        // Make sure game over text is hidden
        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if we hit an NPC
        if (other.CompareTag("NPC"))
        {
            TakeDamage(10);
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateUI();
        
        Debug.Log("Ouch! Health: " + currentHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void Heal(int amount)
    {
        currentHealth += amount;
        
        // Don't go over max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        UpdateUI();
        Debug.Log("Healed! Health: " + currentHealth);
    }
    
    void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }
    
    void Die()
    {
        Debug.Log("Game Over!");
        
        // Show game over text
        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }
        
        // Pause the game
        Time.timeScale = 0;
    }
}