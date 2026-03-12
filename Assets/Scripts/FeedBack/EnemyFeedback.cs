using UnityEngine;

public class EnemyFeedback : MonoBehaviour
{
    [SerializeField] private GameObject deathVFXPrefab;

    [SerializeField] private AudioClip[] deathSFX;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool destroyEnemyObject = false;
    private bool hasPlayed = false;

    [SerializeField] private bool isDead = false;
    private bool previousIsDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead && !previousIsDead)
        {
            PlayDeathFeedback();
        }
        else if (!isDead && previousIsDead)
        {
            ResetFeedback();
        }

        previousIsDead = isDead;
    }

    public void PlayDeathFeedback()
    {
        if (hasPlayed) return;
        hasPlayed = true;


        
        GameObject vfx = Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
        Destroy(vfx, 2f);
        
        AudioClip clip = deathSFX[Random.Range(0, deathSFX.Length)];
        audioSource.pitch = Random.Range(0.9f, 1.5f);
        audioSource.PlayOneShot(clip);

        if (destroyEnemyObject)
        {
            Destroy(gameObject);
        }

    }

    public void ResetFeedback()
    {
        hasPlayed = false;
    }
}
