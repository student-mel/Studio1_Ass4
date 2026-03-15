using UnityEngine;
using System.Collections;

public class StarCollectFeedback : MonoBehaviour
{
    [SerializeField] private bool isCollected = false;
    private bool previousIsCollected = false;

    [SerializeField] private GameObject collectVFXPrefab;
    [SerializeField] private AudioClip[] collectSFX;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private SpriteRenderer starSprite;
    [SerializeField] private bool hideStarOnCollect = true;

    [SerializeField] private float destroyDelay = 0.6f;
    private bool hasPlayed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (isCollected && !previousIsCollected)
        //{
        //    PlayCollectFeedback();
        //}
        //else if (!isCollected && previousIsCollected)
        //{
        //    ResetFeedback();
        //}

        //previousIsCollected = isCollected;
    }

    public void PlayCollectFeedback()
    {
        if (isCollected) return;
        isCollected = true;

        if (collectVFXPrefab != null)
        {
            GameObject vfx = Instantiate(collectVFXPrefab, transform.position, Quaternion.identity);
            Destroy(vfx, 2f);
        }

        if (collectSFX != null && collectSFX.Length > 0 && audioSource != null)
        {
            AudioClip clip = collectSFX[Random.Range(0, collectSFX.Length)];
            if (clip != null)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.PlayOneShot(clip);
            }
        }

        StartCoroutine(CollectAndDestroy());
    }

    private IEnumerator CollectAndDestroy()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        if (starSprite != null)
        {
            starSprite.enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

}
