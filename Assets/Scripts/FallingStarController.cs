using UnityEngine;

public class FallingStarController : CosmicObjectController
{
    ScoreHandler scoreHandler;

    [Tooltip("Use this to define how much falling stars are worth")] public int fallingStarScore = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreHandler = GameObject.Find("Canvas").GetComponent<ScoreHandler>();
        maxAngleSpread = 45f;
        DeployObject(Vector2.down);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag(playerTag))
        {
            scoreHandler.AddScore(fallingStarScore); // Increment the player's score when they collect a falling star
            Destroy(gameObject); // Destroy the falling star
        }
        else if (collision.gameObject.CompareTag(projectileTag))
        {
            //stop the falling star's movement and make it fall straight down when hit by a projectile, but don't destroy it
            rb.linearVelocity = Vector2.zero; // Stop the falling star's current movement
            rb.linearVelocity = Vector2.down * moveSpeed; // Redeploy the falling star to fall straight down
        }
    }
}
