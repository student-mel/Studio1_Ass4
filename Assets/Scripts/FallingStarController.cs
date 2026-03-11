using UnityEngine;

public class FallingStarController : MonoBehaviour
{
    public float moveSpeed;

    public float maxAngleSpread = 45f; // Max angle (in degrees) from the pure down direction

    public const string playerTag = "Player";
    public const string projectileTag = "Bullet";
    public const string starsTag = "Cosmic";
    public const string wallTag = "Wall";
    public const string groundTag = "Ground";

    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float randomAngle = Random.Range(-maxAngleSpread, maxAngleSpread); //generate a random down-ish angle

        Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);
        Vector2 randomDirection = rotation * Vector2.down;

        randomDirection.Normalize();

        //transform.rotation = Quaternion.LookRotation(randomDirection, Vector3.forward); // Rotate the meteor to face the random direction

        float angle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Rotate the meteor to face the random direction (subtracting 90 degrees to align with the sprite's orientation)

        rb.linearVelocity = randomDirection * moveSpeed; //start it moving right away
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Handle collision with player
            Debug.Log("Hit player with falling star");
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
            Debug.Log("Hit stage wall with falling star");
            Destroy(gameObject); // Destroy the meteor
        }

        else if (collision.gameObject.CompareTag(groundTag))
        {
            // Handle collision with the ground
            Debug.Log("Hit ground with falling star");
            Destroy(gameObject); // Destroy the meteor
        }
    }
}
