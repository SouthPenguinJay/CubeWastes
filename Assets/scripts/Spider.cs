using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed = 3f; // Speed at which the enemy follows the player
    public float stoppingDistance = 1f; // Distance at which the enemy stops following
    public float jumpForce = 5f; // Force applied for jumping
    public float jumpInterval = 2f; // Interval between jumps

    private Rigidbody rb;
    private float lastJumpTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastJumpTime = -jumpInterval; // Initialize to allow the first jump immediately
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = player.position - transform.position;
            float distance = direction.magnitude;

            // If the enemy is farther than the stopping distance, move towards the player
            if (distance > stoppingDistance)
            {
                // Normalize the direction and move the enemy
                direction.Normalize();
                Vector3 movement = direction * speed * Time.deltaTime;
                rb.MovePosition(transform.position + movement);
            }

            // Check if it's time to jump
            if (Time.time - lastJumpTime >= jumpInterval)
            {
                Jump();
                lastJumpTime = Time.time;
            }
        }
        else
        {
            Debug.LogError("Player transform is not assigned.");
        }
    }

    void Jump()
    {
        // Apply a vertical force to make the enemy jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
