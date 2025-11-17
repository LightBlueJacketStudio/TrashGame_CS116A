using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private NPCWander[] allNPCs;
    private CarMovement[] allCars;
    
    void Start()
    {
        // Find all NPCs and Cars
        allNPCs = FindObjectsOfType<NPCWander>();
        allCars = FindObjectsOfType<CarMovement>();
        
        Debug.Log("NPCManager found " + allNPCs.Length + " NPCs and " + allCars.Length + " cars");
    }
    
    public void FreezeAllNPCs()
    {
        // Freeze all walking NPCs
        foreach (NPCWander npc in allNPCs)
        {
            npc.SetFrozen(true);
        }
        
        // Freeze all cars
        foreach (CarMovement car in allCars)
        {
            car.SetFrozen(true);
        }
        
        Debug.Log("All NPCs and cars frozen!");
    }
    
    public void UnfreezeAllNPCs()
    {
        // Unfreeze all walking NPCs
        foreach (NPCWander npc in allNPCs)
        {
            npc.SetFrozen(false);
        }
        
        // Unfreeze all cars
        foreach (CarMovement car in allCars)
        {
            car.SetFrozen(false);
        }
        
        Debug.Log("All NPCs and cars unfrozen!");
    }
}