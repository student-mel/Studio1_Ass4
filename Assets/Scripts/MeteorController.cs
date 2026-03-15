using UnityEngine;

public class MeteorController : CosmicObjectController
{
    ScoreHandler scoreHandler;
    [HideInInspector] public StarSpawner spawner;

    [Tooltip("Use this to define how much meteors damage")] public int meteorScore = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreHandler = GameObject.Find("Canvas").GetComponent<ScoreHandler>();
        maxAngleSpread = 45f;
        DeployObject(Vector2.down);

        Vector2 dir = rb.linearVelocity.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag(playerTag))
        {
            scoreHandler.AddScore(-meteorScore); // Decrement the player's score when they get hit by a meteor

            PlayerFeedback playerFeedback = collision.GetComponent<PlayerFeedback>();
            if (playerFeedback != null)
            {
                playerFeedback.PlayHitFeedback();
            }

            Destroy(gameObject); // Destroy the meteor
        }
        else if (collision.gameObject.CompareTag(projectileTag))
        {
            scoreHandler.AddScore(meteorScore);
            spawner.MeteorDestroyed(transform);
            Destroy(gameObject); // Destroy the meteor
            collision.gameObject.SetActive(false); // deactivate the projectile on collision
        }
    }
}
