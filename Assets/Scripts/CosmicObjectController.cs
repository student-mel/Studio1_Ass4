using UnityEngine;

public class CosmicObjectController : MonoBehaviour
{

    public float moveSpeed;

    protected float maxAngleSpread = 45f; // Max angle (in degrees) from the pure down direction

    protected const string playerTag = "Player";
    protected const string projectileTag = "Bullet";
    protected const string starsTag = "Cosmic";
    protected const string wallTag = "Wall";
    protected const string groundTag = "Ground";

    protected Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    protected void DeployObject(Vector2 direction)
    {
        float randomAngle = Random.Range(-maxAngleSpread, maxAngleSpread); //generate a random down-ish angle

        Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);
        Vector2 randomDirection = rotation * direction;

        randomDirection.Normalize();

        //transform.rotation = Quaternion.LookRotation(randomDirection, Vector3.forward); // Rotate the meteor to face the random direction

        float angle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Rotate the meteor to face the random direction (subtracting 90 degrees to align with the sprite's orientation)

        rb.linearVelocity = randomDirection * moveSpeed; //start it moving right away
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Handle collision with player
            Debug.Log("Hit player with meteor");
            Destroy(gameObject); // Destroy the meteor
        }
        else if (collision.gameObject.CompareTag(projectileTag))
        {
            // Handle collision with projectile
            Destroy(gameObject); // Destroy the meteor
        }
        else if (collision.gameObject.CompareTag(starsTag))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>()); // Ignore collision with stars
        }
        else if (collision.gameObject.CompareTag(wallTag))
        {
            //if it hits here, its trajectory is on a path away from the scene so just destroy it since its no longer relevant to the player
            Debug.Log("Hit stage wall with meteor");
            Destroy(gameObject); // Destroy the meteor
        }

        else if (collision.gameObject.CompareTag(groundTag))
        {
            // Handle collision with the ground
            Debug.Log("Hit ground with meteor");
            Destroy(gameObject); // Destroy the meteor
        }
    }
}
