using UnityEngine;
using System.Collections;

public class PlayerFeedback : MonoBehaviour
{
    private bool previousIsHurt = false;

    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] hurtSFX;

    [SerializeField] private Color hurtColor = Color.red;
    [SerializeField] private int flashCount = 3;
    [SerializeField] private float flashDuration = 0.08f;

    private Color originalColor;
    private Coroutine flashRoutine;
    private bool hasPlayed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalColor = playerSprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isHurt && !previousIsHurt)
        //{
        //    PlayHitFeedback();
        //}
        //else if (!isHurt && previousIsHurt)
        //{
        //    ResetFeedback();
        //}

        //previousIsHurt = isHurt;
    }

    public void PlayHitFeedback()
    {
        //if (hasPlayed) return;
        //hasPlayed = true;


        if (hurtSFX.Length > 0)
        {
            AudioClip clip = hurtSFX[Random.Range(0, hurtSFX.Length)];
            audioSource.pitch = Random.Range(0.9f, 1.3f);
            audioSource.PlayOneShot(clip);
        }
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashSprite());
    }

    IEnumerator FlashSprite()
    {
        for (int i = 0; i < flashCount; i++)
        {
            playerSprite.color = hurtColor;
            yield return new WaitForSeconds(flashDuration);

            playerSprite.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
        flashRoutine = null;
    }

    //public void ResetFeedback()
    //{
    //    hasPlayed = false;

    //    playerSprite.color = originalColor;
    //}
}
