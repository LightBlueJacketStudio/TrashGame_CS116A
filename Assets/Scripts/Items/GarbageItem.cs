using UnityEngine;

public class GarbageItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("GarbageItem: Garbage Trigger with: " + other.name);
            TrashCounter.Instance.AddTrash();
            Destroy(gameObject);
        }
    }
}