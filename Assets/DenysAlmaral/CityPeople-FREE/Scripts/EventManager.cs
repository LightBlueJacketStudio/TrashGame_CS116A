using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
    [Header("Blackout Event Settings")]
    public float blackoutDuration = 10f;
    public float timeBetweenBlackouts = 30f;
    public bool autoStartBlackouts = true;
    
    [Header("Visual Effects")]
    public Light directionalLight;
    public float normalLightIntensity = 1f;
    public float blackoutLightIntensity = 0.1f;
    
    private bool isBlackoutActive = false;
    private NPCManager npcManager;
    
    void Start()
    {
        // Find the directional light if not assigned
        if (directionalLight == null)
        {
            directionalLight = FindObjectOfType<Light>();
        }
        
        if (directionalLight != null)
        {
            normalLightIntensity = directionalLight.intensity;
        }
        
        // Find NPC Manager
        npcManager = FindObjectOfType<NPCManager>();
        
        // Start blackout cycle
        if (autoStartBlackouts)
        {
            StartCoroutine(BlackoutCycle());
        }
    }
    
    IEnumerator BlackoutCycle()
    {
        while (true)
        {
            // Wait before next blackout
            yield return new WaitForSeconds(timeBetweenBlackouts);
            
            // Start blackout
            StartBlackout();
            
            // Wait for blackout duration
            yield return new WaitForSeconds(blackoutDuration);
            
            // End blackout
            EndBlackout();
        }
    }
    
    public void StartBlackout()
    {
        Debug.Log("BLACKOUT STARTED!");
        isBlackoutActive = true;
        
        // Dim the lights
        if (directionalLight != null)
        {
            directionalLight.intensity = blackoutLightIntensity;
        }
        
        // Freeze NPCs
        if (npcManager != null)
        {
            npcManager.FreezeAllNPCs();
        }
        
        // Disable NPC collision damage
        DisableNPCDamage();
    }
    
    public void EndBlackout()
    {
        Debug.Log("BLACKOUT ENDED!");
        isBlackoutActive = false;
        
        // Restore lights
        if (directionalLight != null)
        {
            directionalLight.intensity = normalLightIntensity;
        }
        
        // Unfreeze NPCs
        if (npcManager != null)
        {
            npcManager.UnfreezeAllNPCs();
        }
        
        // Re-enable NPC collision damage
        EnableNPCDamage();
    }
    
    void DisableNPCDamage()
    {
        // Find all NPCs and disable their colliders
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcs)
        {
            Collider col = npc.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
            }
        }
    }
    
    void EnableNPCDamage()
    {
        // Re-enable all NPC colliders
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcs)
        {
            Collider col = npc.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = true;
            }
        }
    }
    
    public bool IsBlackoutActive()
    {
        return isBlackoutActive;
    }
}