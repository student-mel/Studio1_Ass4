using UnityEngine;
using System.Collections;

public class EnemyFeedback : MonoBehaviour
{
    [SerializeField] private GameObject deathVFXPrefab;

    [SerializeField] private AudioClip[] deathSFX;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool destroyEnemyObject = true;
    [SerializeField] private float destroyDelay = 0.15f;

    private bool hasPlayed = false;

    private bool isDying = false;
    //[SerializeField] private bool isDead = false;
    private bool previousIsDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (isDead && !previousIsDead)
        //{
        //    PlayDeathFeedback();
        //}
        //else if (!isDead && previousIsDead)
        //{
        //    ResetFeedback();
        //}

        //previousIsDead = isDead;
    }

    public void PlayDeathFeedback()
    {
        if (isDying) return;
        isDying = true;


        
        GameObject vfx = Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
        Destroy(vfx, 2f);
        
        AudioClip clip = deathSFX[Random.Range(0, deathSFX.Length)];
        audioSource.pitch = Random.Range(0.9f, 1.5f);
        audioSource.PlayOneShot(clip);

        if (destroyEnemyObject)
        {
            StartCoroutine(DestroyAfterDelay());
        }

    }

    private IEnumerator DestroyAfterDelay()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = false;
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        

        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    //public void ResetFeedback()
    //{
    //    hasPlayed = false;
    //}
}
