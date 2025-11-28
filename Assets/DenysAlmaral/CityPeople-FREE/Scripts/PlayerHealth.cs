using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
{
    Debug.Log($"OnCollisionEnter called! Hit: {collision.gameObject.name}");
}
    [Header("Health Settings")]
    private int maxHealth = 999999999;           // Maximum hearts (3 by default)
    public int currentHealth;           // Current health
    
    [Header("Damage Settings")]
    public float invincibilityTime = 1f; // Time invincible after taking damage
    private float invincibilityTimer = 0f;
    
    [Header("Events")]
    public UnityEvent onHealthChanged;   // Triggered when health changes
    public UnityEvent onDeath;           // Triggered when player dies

    [Header("UI")]
    public TextMesh healthText;  // Add this at the top with other public variables
    public GameObject gameOverText;

    private EventManager eventManager;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        
        // Hide game over text at start
        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }
        
        // Find EventManager
        eventManager = FindObjectOfType<EventManager>();
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }

    void Update()
    {
        // Update invincibility timer
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter called! Hit object: {other.gameObject.name}, Tag: {other.tag}");
        
        // Take damage when touching NPCs or Cars
        if (other.CompareTag("NPC"))
        {
            Debug.Log("Tag matches NPC!");
            
            // Check if blackout is active - no damage during blackout!
            if (eventManager != null && eventManager.IsBlackoutActive())
            {
                Debug.Log("Blackout is active - no damage taken!");
                return;
            }
            
            if (invincibilityTimer <= 0)
            {
                Debug.Log("Not invincible - taking damage!");
                TakeDamage(10);
            }
            else
            {
                Debug.Log($"Still invincible! Timer: {invincibilityTimer}");
            }
        }
        else
        {
            Debug.Log($"Tag doesn't match. Expected 'NPC', got '{other.tag}'");
        }
    }

    // OPTIONAL: Continuous damage while in contact
    void OnTriggerStay(Collider other)  // Changed from Collider2D to Collider
    {
        // Uncomment if you want continuous damage
        // if ((other.CompareTag("NPC") || other.CompareTag("Car")) && invincibilityTimer <= 0)
        // {
        //     TakeDamage(1);
        // }
    }

    public void TakeDamage(int damage)
    {
        if (invincibilityTimer > 0)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        Debug.Log($"Player took {damage} damage! Health: {currentHealth}/{maxHealth}");

        invincibilityTimer = invincibilityTime;
        
        UpdateHealthUI();  // Add this line
        
        onHealthChanged?.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Don't exceed max

        Debug.Log($"Health restored! +{amount} hearts. Health: {currentHealth}/{maxHealth}");

        UpdateHealthUI();  // Explicitly update UI
        
        // Trigger event for UI update
        onHealthChanged?.Invoke();
    }

    // For backwards compatibility with existing HealthPickup script
    public void Heal(int amount)
    {
        RestoreHealth(amount);
    }

    void Die()
    {
        Debug.Log("Player died!");
        
        // Show game over text
        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }
        
        onDeath?.Invoke();
        
        // Freeze the player
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    // Get current health percentage for UI
    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }

    // Check if at full health (don't spawn hearts if full)
    public bool IsFullHealth()
    {
        return currentHealth >= maxHealth;
    }

    public bool IsInvincible()
    {
        return invincibilityTimer > 0;
    }
}