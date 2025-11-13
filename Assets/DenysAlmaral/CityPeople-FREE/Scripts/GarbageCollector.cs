using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    [Header("Collection Settings")]
    public int garbageCollected = 0;
    
    [Header("UI")]
    public TextMesh counterText;
    
    void Start()
    {
        UpdateUI();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Garbage"))
        {
            garbageCollected++;
            
            UpdateUI();
            
            Debug.Log("Garbage collected! Total: " + garbageCollected);
            
            Destroy(other.gameObject);
        }
    }
    
    void UpdateUI()
    {
        if (counterText != null)
        {
            counterText.text = "Trash Collected: " + garbageCollected;
        }
    }
}