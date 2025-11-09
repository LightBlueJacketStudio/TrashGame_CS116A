using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    [Header("Collection Settings")]
    public int garbageCollected = 0;
    
    void OnTriggerEnter(Collider other)
    {
        // Check if we touched garbage
        if (other.CompareTag("Garbage"))
        {
            // Increase count
            garbageCollected++;
            
            // Print to console
            Debug.Log("Garbage collected! Total: " + garbageCollected);
            
            // Destroy the garbage object
            Destroy(other.gameObject);
        }
    }
}