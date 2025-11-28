using UnityEngine;
using TMPro; // Add this for TextMesh Pro

public class TrashCounter : MonoBehaviour
{
    public static TrashCounter Instance;
    public TextMeshPro tmpText; // Use TextMesh Pro for UI

    int count = 0;

    void Awake()
    {
        Instance = this;
        UpdateLabel();
    }

    public void AddTrash()
    {
        Debug.Log("Trash collected!, count : " + count);
        count++;
        UpdateLabel();
    }

    void UpdateLabel()
    {
        tmpText.text = "Trash Collected: " + count;
    }
}