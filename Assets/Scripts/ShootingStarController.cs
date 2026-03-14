using UnityEngine;

public class ShootingStarController : CosmicObjectController
{
    ScoreHandler scoreHandler;

    [Tooltip("Use this to define how much shooting stars are worth")] public int shootingStarScore = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreHandler = GameObject.Find("Canvas").GetComponent<ScoreHandler>();
        maxAngleSpread = 10f; //narrow angle spread for shooting stars, since they should be more predictable than meteors
    }

    public void SetDirectionAndDeploy(Vector2 direction)
    {
        DeployObject(direction);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag(projectileTag))
        {
            Destroy(gameObject); // Destroy the shooting star when hit by a projectile
            //deploy code fot the shooting star's power up goodies once implemented
            scoreHandler.AddScore(shootingStarScore); // Increment the player's score by 2 when they hit a shooting star with a projectile, since shooting stars are more difficult to hit than falling stars
            collision.gameObject.SetActive(false); // deactivate the projectile on collision
        }
    }
}
