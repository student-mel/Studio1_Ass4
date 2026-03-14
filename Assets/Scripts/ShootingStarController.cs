using UnityEngine;

public class ShootingStarController : CosmicObjectController
{
    ScoreHandler scoreHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreHandler = GameObject.Find("Canvas").GetComponent<ScoreHandler>();
        maxAngleSpread = 10f; //narrow angle spread for shooting stars, since they should be more predictable than meteors
        moveSpeed = 10f;
    }

    public void SetDirectionAndDeploy(Vector2 direction)
    {
        DeployObject(direction);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag(projectileTag))
        {
            Destroy(gameObject); // Destroy the shooting star when hit by a projectile
            //deploy code fot the shooting star's power up goodies once implemented
            scoreHandler.AddScore(2); // Increment the player's score by 2 when they hit a shooting star with a projectile, since shooting stars are more difficult to hit than falling stars
        }
    }
}
