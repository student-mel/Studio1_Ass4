using UnityEngine;

public class FallingStarController : CosmicObjectController
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxAngleSpread = 45f;
        DeployObject(Vector2.down);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
