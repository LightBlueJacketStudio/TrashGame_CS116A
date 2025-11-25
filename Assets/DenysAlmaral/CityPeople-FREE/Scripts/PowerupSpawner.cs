using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [Header("Powerup Prefabs")]
    public GameObject goldCoinPrefab;      // 12% satisfaction boost
    public GameObject dollarBillPrefab;    // 10% satisfaction boost
    public GameObject jewelryPrefab;       // 5% satisfaction boost
    public GameObject redbullPrefab;       // Speed boost
    public GameObject heartPrefab;         // Health restoration

    [Header("Spawn Settings")]
    public float spawnInterval = 10f;      // Spawn every 10 seconds
    public float spawnRadius = 20f;        // How far from player to spawn
    public int maxPowerupsOnScreen = 5;    // Maximum powerups at once
    
    [Header("Boundary Settings")]
    public bool useBoundaries = true;      // Enable/disable boundary checking
    public float minX = -50f;              // Minimum X position
    public float maxX = 50f;               // Maximum X position
    public float minZ = -50f;              // Minimum Z position
    public float maxZ = 50f;               // Maximum Z position
    
    [Header("Spawn Chances")]
    [Range(0f, 1f)]
    public float goldCoinChance = 0.05f;   // 5% chance
    [Range(0f, 1f)]
    public float dollarBillChance = 0.10f; // 10% chance
    [Range(0f, 1f)]
    public float jewelryChance = 0.12f;    // 12% chance
    [Range(0f, 1f)]
    public float redbullChance = 0.08f;    // 8% chance
    [Range(0f, 1f)]
    public float heartChance = 0.15f;      // 15% chance (common health pickup)

    private Transform playerTransform;
    private List<GameObject> activePowerups = new List<GameObject>();

    void Start()
    {
        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("PowerupSpawner: Player not found! Make sure Player has 'Player' tag.");
        }

        // Start spawning powerups
        StartCoroutine(SpawnPowerupsRoutine());
    }

    IEnumerator SpawnPowerupsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Remove null references (collected powerups)
            activePowerups.RemoveAll(item => item == null);

            // Only spawn if under the limit
            if (activePowerups.Count < maxPowerupsOnScreen && playerTransform != null)
            {
                SpawnRandomPowerup();
            }
        }
    }

    void SpawnRandomPowerup()
    {
        // Randomly decide which powerup to spawn based on chances
        float randomValue = Random.value;

        GameObject prefabToSpawn = null;

        if (randomValue < goldCoinChance && goldCoinPrefab != null)
        {
            prefabToSpawn = goldCoinPrefab;
        }
        else if (randomValue < goldCoinChance + dollarBillChance && dollarBillPrefab != null)
        {
            prefabToSpawn = dollarBillPrefab;
        }
        else if (randomValue < goldCoinChance + dollarBillChance + jewelryChance && jewelryPrefab != null)
        {
            prefabToSpawn = jewelryPrefab;
        }
        else if (randomValue < goldCoinChance + dollarBillChance + jewelryChance + redbullChance && redbullPrefab != null)
        {
            prefabToSpawn = redbullPrefab;
        }
        else if (randomValue < goldCoinChance + dollarBillChance + jewelryChance + redbullChance + heartChance && heartPrefab != null)
        {
            prefabToSpawn = heartPrefab;
        }

        if (prefabToSpawn != null)
        {
            // Spawn at random position around player
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject powerup = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            activePowerups.Add(powerup);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Get random position in a circle around the player
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = playerTransform.position + new Vector3(randomCircle.x, 1.5f, randomCircle.y);
        
        // Clamp to boundaries if enabled
        if (useBoundaries)
        {
            spawnPos.x = Mathf.Clamp(spawnPos.x, minX, maxX);
            spawnPos.z = Mathf.Clamp(spawnPos.z, minZ, maxZ);
        }
        
        return spawnPos;
    }

    void OnDrawGizmosSelected()
    {
        if (playerTransform != null)
        {
            // Draw spawn radius
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(playerTransform.position, spawnRadius);
        }
        
        // Draw boundaries
        if (useBoundaries)
        {
            Gizmos.color = Color.red;
            
            // Draw boundary box
            Vector3 center = new Vector3((minX + maxX) / 2f, 1.5f, (minZ + maxZ) / 2f);
            Vector3 size = new Vector3(maxX - minX, 0.1f, maxZ - minZ);
            Gizmos.DrawWireCube(center, size);
        }
    }
}