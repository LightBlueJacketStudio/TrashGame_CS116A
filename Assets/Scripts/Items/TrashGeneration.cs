using UnityEngine;

public class GarbageSpawner3D : MonoBehaviour
{
    public GameObject garbagePrefab;
    public int amount = 1;

    public Vector3 areaCenter = new Vector3(0,0,0);
    public Vector3 areaSize = new Vector3(50,0, 50);

    public LayerMask obstacleMask;

    public float minSpacing = 1f; 

    void Start()
    {
        SpawnGarbage();
    }

    void SpawnGarbage()
    {
        int spawned = 0;
        int attempts = 0;

        while (spawned < amount && attempts < amount * 10)
        {
            attempts++;

            Vector3 pos = GetRandomPosition();

            // Check if position overlaps with buildings, NPCs, etc.
            if (Physics.CheckSphere(pos, minSpacing, obstacleMask))
                continue;

            Instantiate(garbagePrefab, pos, Quaternion.identity);
            spawned++;
        }
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2),
            areaCenter.y,
            Random.Range(areaCenter.z - areaSize.z / 2, areaCenter.z + areaSize.z / 2)
        );
    }
}
