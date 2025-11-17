using UnityEngine;

public class NPCWander : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float changeDirectionTime = 3f;
    
    [Header("Boundary Settings")]
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;
    
    private Vector3 moveDirection;
    private float timer;
    private Animator animator;
    private bool isFrozen = false; 
    
    void Start()
    {
        animator = GetComponent<Animator>();
        ChooseNewDirection();
    }
    
    void Update()
    {
        // Don't move if frozen 
        if (isFrozen)
        {
            return;
        }
        
        // Move the NPC
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        
        // Check boundaries and turn around if needed
        Vector3 pos = transform.position;
        if (pos.x < minX || pos.x > maxX || pos.z < minZ || pos.z > maxZ)
        {
            // Turn around
            moveDirection = -moveDirection;
            transform.position = new Vector3(
                Mathf.Clamp(pos.x, minX, maxX),
                pos.y,
                Mathf.Clamp(pos.z, minZ, maxZ)
            );
        }
        
        // Face the direction we're moving
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
        
        // Change direction after timer
        timer += Time.deltaTime;
        if (timer >= changeDirectionTime)
        {
            ChooseNewDirection();
            timer = 0;
        }
        
        // Set animator walking state
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }
    }
    
    void ChooseNewDirection()
    {
        // Pick a random direction
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        
        moveDirection = new Vector3(randomX, 0, randomZ).normalized;
    }
    
    public void SetFrozen(bool frozen)
    {
        isFrozen = frozen;
        
        // Stop animator when frozen
        if (animator != null)
        {
            animator.SetBool("isWalking", !frozen);
        }
    }
}