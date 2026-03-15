using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class StarSpawner : MonoBehaviour
{
    //this is going to spawn falling stars and meteors that the player can either collect or shoot. 
    //they will spawn at random intervals and fall at a constant speed but random angles and positions.

    [Tooltip("Attach the falling star prefab here")] public GameObject fallingStarPrefab; // Prefab for the falling star
    [Tooltip("Attach the meteor Prefab here")] public GameObject meteorPrefab; // Prefab for the meteor

    [Tooltip("Set the falling star's speed here")]  public float fallingStarSpeed = 5.0f;
    [Tooltip("Set the meteor's speed here")]  public float meteorSpeed = 5.0f;

    [Tooltip("This defines the falling star to meteor ratio. Set this smaller for less falling stars")] 
    public float fallingStarToMeteorRatio = 0.5f;

    private BoxCollider2D spawnerBounds;

    [Tooltip("This governs how many falling stars and meteors are spawned per second")]  public float spawnFrequency = 10f; //how many stars to spawn per second
    private float spawnInterval; // Time between spawns
    private float spawnTimer; // Timer to track time since last spawn
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Meteor Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] meteorSpawnClips;

    public SpellsManager spellManager;
    public int meteorsShot = 0;
    
    void Awake()
    {
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
    void Start()
    {
        spawnInterval = 1f/spawnFrequency; // Calculate the time between spawns based on the frequency
        //spawnTimer = Time.time + spawnInterval; // Initialize the spawn timer
        spawnTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //while (Time.time >= spawnTimer)
        while (spawnTimer >= spawnInterval)
        {
            //spawn a falling star or meteor at a random position and angle within the spawn area
            Bounds bounds = spawnerBounds.bounds;

            Vector2 randomPosition = new Vector2(
               Random.Range(bounds.min.x, bounds.max.x),
               Random.Range(bounds.min.y, bounds.max.y));

            if (Random.value < fallingStarToMeteorRatio) // 50% chance to spawn a falling star or meteor by default
            {
                GameObject fallingStar = GameObject.Instantiate(fallingStarPrefab, randomPosition, Quaternion.identity);
                fallingStar.GetComponent<FallingStarController>().moveSpeed = fallingStarSpeed;
            }
            else
            {
                GameObject meteor = GameObject.Instantiate(meteorPrefab, randomPosition, Quaternion.identity);
                MeteorController m = meteor.GetComponent<MeteorController>();
                m.moveSpeed = meteorSpeed;
                m.spawner = this;

                if (meteorSpawnClips.Length > 0 && audioSource != null)
                {
                    AudioClip clip = meteorSpawnClips[Random.Range(0, meteorSpawnClips.Length)];
                    audioSource.PlayOneShot(clip);
                }
            }

            // Schedule the next spawn here
            //spawnTimer += spawnInterval;
            spawnTimer = 0f; // Reset the spawn timer after spawning an object
        }
        spawnTimer += Time.deltaTime; // Increment the spawn timer by the time elapsed since the last frame
    }
    
    public void MeteorDestroyed(Transform t)
    {
        SpellStats s;
        if (spellManager.CheckSpellSpawn(meteorsShot, out s))
        {
            s.Spawn(t, spellManager);
            // reset meteorsShot for now, for testing only, absolutely will not work if we have more than one spell
            meteorsShot = 0;    
        }

        meteorsShot++;
    }
}
