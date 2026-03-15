using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CosmicObjectController : MonoBehaviour
{

    public float moveSpeed;

    protected float maxAngleSpread; // Max angle (in degrees) from the pure cardinal direction

    protected const string playerTag = "Player";
    protected const string projectileTag = "Bullet";
    protected const string starsTag = "Cosmic";
    protected const string wallTag = "Wall";
    protected const string groundTag = "Ground";

    [Header("Meteor Impact Audio")]
    [SerializeField] private AudioClip[] meteorImpactClips;

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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Handle collision with player
            //Debug.Log("Hit player with meteor");
            //Destroy(gameObject); // Destroy this object, but the player will handle the consequences of being hit in their own script
        }
        else if (collision.gameObject.CompareTag(projectileTag))
        {
            // Handle collision with projectile
        }
        else if (collision.gameObject.CompareTag(starsTag))
        {
            //Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>()); // Ignore collision with stars
        }
        else if (collision.gameObject.CompareTag(wallTag))
        {
            //if it hits here, its trajectory is on a path away from the scene so just destroy it since its no longer relevant to the player
            //Debug.Log("Hit stage wall with meteor");
            //.IgnoreCollision(collision.collider, GetComponent<Collider2D>()); // Ignore collision with stage walls
            Destroy(gameObject); // Destroy the object since it's no longer relevant to the player
        }

        else if (collision.gameObject.CompareTag(groundTag))
        {
            // Handle collision with the ground
            //Debug.Log("Hit ground with meteor");

            if (meteorImpactClips.Length > 0)
            {
                AudioClip clip = meteorImpactClips[Random.Range(0, meteorImpactClips.Length)];
                AudioSource.PlayClipAtPoint(clip, transform.position);
            }

            Destroy(gameObject); // Destroy the object since it's no longer relevant to the player
        }
    }
}
