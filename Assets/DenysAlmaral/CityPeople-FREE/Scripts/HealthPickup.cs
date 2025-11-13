using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public int healthAmount = 20;
    
    [Header("Visual Settings")]
    public float rotateSpeed = 50f;
    public float bobSpeed = 2f;
    public float bobHeight = 0.3f;
    
    private Vector3 startPos;
    
    void Start()
    {
        startPos = transform.position;
    }
    
    void Update()
    {
        // Rotate the pickup
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        
        // Bob up and down
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if player touched it
        if (other.CompareTag("Player"))
        {
            // Get PlayerHealth component
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            
            if (playerHealth != null)
            {
                playerHealth.Heal(healthAmount);
                Debug.Log("Health restored! +" + healthAmount);
                
                // Destroy the pickup
                Destroy(gameObject);
            }
        }
    }
}