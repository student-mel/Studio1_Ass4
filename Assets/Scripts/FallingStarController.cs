using UnityEngine;

public class FallingStarController : CosmicObjectController
{
    ScoreHandler scoreHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreHandler = GameObject.Find("Canvas").GetComponent<ScoreHandler>();
        maxAngleSpread = 45f;
        moveSpeed = 5f;
        DeployObject(Vector2.down);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag(playerTag))
        {
            scoreHandler.AddScore(1); // Increment the player's score when they collect a falling star
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
