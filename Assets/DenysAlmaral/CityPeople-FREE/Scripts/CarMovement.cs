using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public Vector3 moveDirection = Vector3.forward;
    
    [Header("Boundary Settings")]
    public float minX = -15f;
    public float maxX = 15f;
    public float minZ = -15f;
    public float maxZ = 15f;
    public bool loopAround = true; 
    private bool isFrozen = false;

    public void SetFrozen(bool frozen)
    {
        isFrozen = frozen;
    }

    void Start()
    {
        // Face the direction we're moving
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
    
    void Update()
    {
         if (isFrozen)
        {
            return;
        }
        // Move the car
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        
        // Check boundaries
        Vector3 pos = transform.position;
        
        if (loopAround)
        {
            // Loop to other side when reaching boundary
            if (pos.x < minX) pos.x = maxX;
            if (pos.x > maxX) pos.x = minX;
            if (pos.z < minZ) pos.z = maxZ;
            if (pos.z > maxZ) pos.z = minZ;
            
            transform.position = pos;
        }
        else
        {
            // Turn around at boundaries
            if (pos.x < minX || pos.x > maxX || pos.z < minZ || pos.z > maxZ)
            {
                moveDirection = -moveDirection;
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
        }
    }
}