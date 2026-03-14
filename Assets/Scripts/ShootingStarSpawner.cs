using UnityEngine;

public class ShootingStarSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Tooltip("Attach the shooting star prefab here")] public GameObject shootingStarPrefab; // Prefab for the shooting star

    [Tooltip("Set the shooting star's speed here")] public float shootingStarSpeed = 10.0f;

    [Tooltip("Set this for the min amount of seconds between shooting stars")] public float minTimeForSpawn = 2.0f;
    [Tooltip("Set this for the max amount of seconds between shooting stars")] public float maxTimeForSpawn = 10.0f;

    [Tooltip("Use these to determine from where to deploy the shooting stars. NOTE: This entire script collapses if it can't find 2 Box Colliders here")]
    [SerializeField] private BoxCollider2D spawnerBounds1, spawnerBounds2;

    [Tooltip("This defines how many are coming from the left vs how many are coming from the right. Set this smaller for more shooting stars from the right")]
    public float leftRightRatio = 0.5f;

    private float spawnInterval; // Time between spawns
    private float spawnTimer; // Timer to track time since last spawn

    Camera cam;

    void Awake()
    {
        cam = Camera.main; // Get the main camera in the scene
        if (shootingStarPrefab == null)
        {
            shootingStarPrefab = Resources.Load<GameObject>("Prefabs/ShootingStar"); // Load the shooting star prefab from the Resources folder
        }
        if (spawnerBounds1 == null || spawnerBounds2 == null)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("ShootingStarSpawner"); // Find all GameObjects with the tag "ShootingStarSpawner"
            if (gameObjects.Length != 2)
            {
                Debug.Log("right amount of spawners not found");
                return; //if there aren't 2 objects tagged as "ShootingStarSpawner", something's gone wrong and we should just exit out of this function to avoid errors
            }
            foreach (GameObject obj in gameObjects)
            {
                BoxCollider2D collider = obj.GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component from the GameObject
                if (collider == null)
                {
                    Debug.Log("box colliders not found");
                    return; //if either tagged object doesn't have a BoxCollider2D component, something's gone wrong and we should just exit out of this function to avoid errors
                }
            }
            spawnerBounds1 = gameObjects[0].GetComponent<BoxCollider2D>(); // Find the left BoxCollider2D component in the scene
            spawnerBounds2 = gameObjects[1].GetComponent<BoxCollider2D>(); // Find the right BoxCollider2D component in the scene
        }
        //place these on the top left and top right sides of the screen, ensuring they are big enough to cover the entire area from the center of the screen edge to the top.
        Vector3 deployLeft = new Vector3(5.0f, (Screen.height - Screen.height / 4), 0);
        Vector3 worldLeft = cam.ScreenToWorldPoint(deployLeft);
        spawnerBounds1.gameObject.transform.position = new Vector3(worldLeft.x, worldLeft.y, 0);

        Vector3 deployRight = new Vector3(Screen.width - 5.0f, (Screen.height - Screen.height / 4), 0); //tiny nudge inwards to avoid contact with wall object righht away
        Vector3 worldRight = cam.ScreenToWorldPoint(deployRight);
        spawnerBounds2.gameObject.transform.position = new Vector3(worldRight.x, worldRight.y, 0);

    }
    void Start()
    {
        if (spawnerBounds1 != null && spawnerBounds2 != null)
        {
            spawnInterval = Random.Range(minTimeForSpawn, maxTimeForSpawn); // Get a random time between the min and max spawn times
            spawnTimer = Time.time + spawnInterval; // Initialize the spawn timer
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerBounds1 == null || spawnerBounds2 == null)
        {
            Debug.Log("Spawners not found");
            return;
        }
        while (spawnTimer >= spawnInterval)
        {
            if (Random.value < leftRightRatio)
            {
                Bounds bounds = spawnerBounds1.bounds;
                Vector2 randomPosition = new Vector2(
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y));

                GameObject shootingStar = GameObject.Instantiate(shootingStarPrefab, randomPosition, Quaternion.identity);
                shootingStar.GetComponent<ShootingStarController>().moveSpeed = shootingStarSpeed;
                shootingStar.GetComponent<ShootingStarController>().SetDirectionAndDeploy(Vector2.right);
            }
            else
            {
                Bounds bounds = spawnerBounds2.bounds;
                Vector2 randomPosition = new Vector2(
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y));

                GameObject shootingStar = GameObject.Instantiate(shootingStarPrefab, randomPosition, Quaternion.identity);
                shootingStar.GetComponent<ShootingStarController>().moveSpeed = shootingStarSpeed;
                shootingStar.GetComponent<ShootingStarController>().SetDirectionAndDeploy(Vector2.left);
            }

            spawnTimer = 0f; // Reset the spawn timer to 0 to ensure that the next spawn is scheduled correctly based on the current time
            spawnInterval = Random.Range(minTimeForSpawn, maxTimeForSpawn); //get a new random spawn time
                                                                            //spawnTimer += spawnInterval; // Update the spawn timer to the next spawn time
        }
        spawnTimer += Time.deltaTime; // Increment the spawn timer by the time that has passed since the last frame
    }
}