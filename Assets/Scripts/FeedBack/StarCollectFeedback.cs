using UnityEngine;

public class StarCollectFeedback : MonoBehaviour
{
    [SerializeField] private bool isCollected = false;
    private bool previousIsCollected = false;

    [SerializeField] private GameObject collectVFXPrefab;
    [SerializeField] private AudioClip[] collectSFX;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private SpriteRenderer starSprite;
    [SerializeField] private bool hideStarOnCollect = true;

    private bool hasPlayed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollected && !previousIsCollected)
        {
            PlayCollectFeedback();
        }
        else if (!isCollected && previousIsCollected)
        {
            ResetFeedback();
        }

        previousIsCollected = isCollected;
    }

    public void PlayCollectFeedback()
    {
        if (hasPlayed) return;
        hasPlayed = true;

        GameObject vfx = Instantiate(collectVFXPrefab, transform.position, Quaternion.identity);
        Destroy(vfx, 2f);
        

        if (collectSFX.Length > 0)
        {
            AudioClip clip = collectSFX[Random.Range(0, collectSFX.Length)];
            audioSource.PlayOneShot(clip);
        }

        if (hideStarOnCollect)
        {
            starSprite.enabled = false;
        }
    }

    public void ResetFeedback()
    {
        hasPlayed = false;
        starSprite.enabled = true;
    }


}
