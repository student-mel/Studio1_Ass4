using UnityEngine;

public class MeteorController : CosmicObjectController
{
    ScoreHandler scoreHandler;

    [Tooltip("Use this to define how much meteors damage")] public int meteorScore = 1;
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
            scoreHandler.AddScore(-meteorScore); // Decrement the player's score when they get hit by a meteor
            Destroy(gameObject); // Destroy the meteor
        }
        else if (collision.gameObject.CompareTag(projectileTag))
        {
            Destroy(gameObject); // Destroy the meteor
        }
    }
}
