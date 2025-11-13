using UnityEngine;

public class TrashCounter : MonoBehaviour
{
    public static TrashCounter Instance;     // global access
    public TextMesh textMesh;                // drag your TextMesh here in Inspector

    int count = 0;

    void Awake()
    {
        Instance = this;
        UpdateLabel();
    }

    public void AddTrash()
    {
        count++;
        UpdateLabel();
    }

    void UpdateLabel()
    {
        textMesh.text = "Trash Collected: " + count;
    }
}
