using UnityEngine;

public enum PowerupType
{
    GoldCoin,      // 12% satisfaction boost
    DollarBill,    // 10% satisfaction boost
    Jewelry,       // 5% satisfaction boost
    Redbull,       // Speed boost
    Heart          // Health restoration
}

public class Powerup : MonoBehaviour
{
    [Header("Powerup Settings")]
    public PowerupType powerupType;
    public float satisfactionBoost = 0f;   // Percentage boost (5, 10, or 12)
    public float speedBoostMultiplier = 0f; // For Redbull (e.g., 1.5 = 50% faster)
    public float speedBoostDuration = 0f;   // How long speed boost lasts
    public int healthRestore = 0;           // For Heart (e.g., 1 or 2 hearts)
    
    [Header("Visual Settings")]
    public float rotationSpeed = 50f;       // Rotate the powerup
    public float bobSpeed = 2f;             // Bob up and down
    public float bobHeight = 0.2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotate the powerup
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Bob up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectPowerup(other.gameObject);
        }
    }

    void CollectPowerup(GameObject player)
    {
        // Handle different powerup types
        switch (powerupType)
        {
            case PowerupType.GoldCoin:
            case PowerupType.DollarBill:
            case PowerupType.Jewelry:
                // Boost satisfaction (you'll need to implement GameManager to track this)
                Debug.Log($"Collected {powerupType}! Satisfaction +{satisfactionBoost}%");
                // TODO: GameManager.Instance.AddSatisfaction(satisfactionBoost);
                break;

            case PowerupType.Redbull:
                // Apply speed boost
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.ApplySpeedBoost(speedBoostMultiplier, speedBoostDuration);
                    Debug.Log($"Speed boost applied! {speedBoostMultiplier}x for {speedBoostDuration} seconds");
                }
                break;

            case PowerupType.Heart:
                // Restore health
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.RestoreHealth(healthRestore);
                    Debug.Log($"Health restored! +{healthRestore} hearts");
                }
                break;
        }

        // Play collection sound (optional)
        // AudioManager.Instance.PlaySound("PowerupCollect");

        // Destroy the powerup
        Destroy(gameObject);
    }
}