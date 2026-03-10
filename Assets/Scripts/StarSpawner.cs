using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    //this is going to spawn falling stars and meteors that the player can either collect or shoot. 
    //they will spawn at random intervals and fall at a constant speed but random angles and positions.

    public GameObject fallingStarPrefab; // Prefab for the falling star
    public GameObject meteorPrefab; // Prefab for the meteor

    [SerializeField]
    private BoxCollider2D spawnerBounds;

    [Tooltip("This governs how many falling stars and meteors are spawned per second")]  public float spawnFrequency = 10f; //how many stars to spawn per second
    private float spawnInterval; // Time between spawns
    private float spawnTimer; // Timer to track time since last spawn
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnInterval = 1f/spawnFrequency; // Calculate the time between spawns based on the frequency
        spawnTimer = Time.time + spawnInterval; // Initialize the spawn timer
        if (fallingStarPrefab == null)
        {
            fallingStarPrefab = Resources.Load<GameObject>("Prefabs/FallingStar"); // Load the falling star prefab from the Resources folder
        }
        if (meteorPrefab == null)
        {
            meteorPrefab = Resources.Load<GameObject>("Prefabs/Meteor"); // Load the meteor prefab from the Resources folder
        }
        spawnerBounds = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component attached to this GameObject
    }

    // Update is called once per frame
    void Update()
    {
        while (Time.time >= spawnTimer)
        {
            //spawn a falling star or meteor at a random position and angle within the spawn area
            Bounds bounds = spawnerBounds.bounds;

            Vector2 randomPosition = new Vector2(
               Random.Range(bounds.min.x, bounds.max.x),
               Random.Range(bounds.min.y, bounds.max.y));

            if (Random.value < 0.5f) // 50% chance to spawn a falling star or meteor
            {
                GameObject.Instantiate(fallingStarPrefab, randomPosition, Quaternion.identity);
            }
            else
            {
                GameObject.Instantiate(meteorPrefab, randomPosition, Quaternion.identity);
            }

            // Schedule the next spawn here
            spawnTimer += spawnInterval;
        }
    }
}
