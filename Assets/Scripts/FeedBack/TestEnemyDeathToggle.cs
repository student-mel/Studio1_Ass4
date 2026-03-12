using UnityEngine;

public class TestEnemyDeathToggle : MonoBehaviour
{
    [Header("Test Toggle")]
    [SerializeField] private bool isDead = false;

    private bool previousIsDead = false;
    private Feedback feedback;

    private void Awake()
    {
        feedback = GetComponent<Feedback>();
        Debug.Log("TestEnemyDeathToggle Awake ran.");
    }

    private void Update()
    {
        if (isDead != previousIsDead)
        {
            Debug.Log("isDead changed to: " + isDead);
        }

        if (isDead && !previousIsDead)
        {
            if (feedback != null)
            {
                Debug.Log("Calling PlayDeathFeedback()");
                feedback.PlayDeathFeedback();
            }
            else
            {
                Debug.LogWarning("Feedback component is missing.");
            }
        }

        previousIsDead = isDead;
    }
}