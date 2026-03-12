using UnityEngine;

public class Feedback : MonoBehaviour
{
    [SerializeField] private GameObject deathVFXPrefab;

    [SerializeField] private AudioClip[] deathSFX;

    [SerializeField] private bool destroyEnemyObject = true;
    private bool hasPlayed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDeathFeedback()
    {
        if (hasPlayed) return;

        hasPlayed = true;

        if (deathVFXPrefab != null)
        {
            GameObject vfx = Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(vfx, 2f);
        }

        if (deathSFX != null && deathSFX.Length > 0)
        {
            AudioClip clip = deathSFX[Random.Range(0, deathSFX.Length)];

            if (clip == null)
            {
                Debug.LogWarning("Selected clip is NULL");
                return;
            }

            Debug.Log("Playing clip: " + clip.name);

            GameObject tempAudio = new GameObject("TempAudio");
            tempAudio.transform.position = Vector3.zero; // safer for testing

            AudioSource source = tempAudio.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = 1f;
            source.pitch = Random.Range(0.95f, 1.05f);
            source.spatialBlend = 0f; // force 2D sound
            source.playOnAwake = false;

            source.Play();

            Destroy(tempAudio, clip.length + 0.5f);
        }
        else
        {
            Debug.LogWarning("deathSFX array is empty or null");
        }
    }
}
