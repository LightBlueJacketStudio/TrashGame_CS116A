using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Boundary Settings")]
    public bool useBoundaries = true;      // Enable/disable boundary checking
    public float minX = -25f;              // Minimum X position
    public float maxX = 25f;               // Maximum X position
    public float minZ = -25f;              // Minimum Z position
    public float maxZ = 25f;               // Maximum Z position

    private Rigidbody rb;
    private Vector3 movement;
    private float currentSpeedMultiplier = 1f;
    private Coroutine speedBoostCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        if (rb == null)
        {
            Debug.LogError("PlayerMovement: Rigidbody component missing!");
        }
        
        // Lock rotation so player doesn't fall over
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    void Update()
    {
        // Get input
        float moveX = Input.GetAxisRaw("Horizontal"); 
        float moveZ = Input.GetAxisRaw("Vertical");   

        // Calculate movement direction
        movement = new Vector3(moveX, 0f, moveZ).normalized;

        // Rotate player to face movement direction
        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody
        if (rb != null)
        {
            Vector3 velocity = movement * moveSpeed * currentSpeedMultiplier;
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        }
        
        // Clamp position to boundaries
        if (useBoundaries)
        {
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
            transform.position = clampedPosition;
        }
    }

    // Called by Powerup script when Redbull is collected
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        // If already boosted, stop the old coroutine
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
        }

        // Start new speed boost
        speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
    }

    IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        currentSpeedMultiplier = multiplier;
        Debug.Log($"Speed boost active! Speed: {moveSpeed * currentSpeedMultiplier}");

        yield return new WaitForSeconds(duration);

        currentSpeedMultiplier = 1f;
        Debug.Log("Speed boost ended. Back to normal speed.");
        speedBoostCoroutine = null;
    }

    // Optional: Get current speed for UI display
    public float GetCurrentSpeed()
    {
        return moveSpeed * currentSpeedMultiplier;
    }

    public bool IsSpeedBoosted()
    {
        return currentSpeedMultiplier > 1f;
    }

    // Draw boundaries in Scene view
    void OnDrawGizmosSelected()
    {
        if (useBoundaries)
        {
            Gizmos.color = Color.cyan;
            
            // Draw boundary box
            Vector3 center = new Vector3((minX + maxX) / 2f, transform.position.y, (minZ + maxZ) / 2f);
            Vector3 size = new Vector3(maxX - minX, 0.1f, maxZ - minZ);
            Gizmos.DrawWireCube(center, size);
        }
    }
}