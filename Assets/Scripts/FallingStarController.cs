using UnityEngine;

public class FallingStarController : CosmicObjectController
{
    ScoreHandler scoreHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreHandler = GameObject.Find("Canvas").GetComponent<ScoreHandler>();
        maxAngleSpread = 45f;
        DeployObject(Vector2.down);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag(playerTag))
        {
            scoreHandler.score++; // Increment the player's score when they collect a falling star
            Destroy(gameObject); // Destroy the falling star
        }
    }
}
