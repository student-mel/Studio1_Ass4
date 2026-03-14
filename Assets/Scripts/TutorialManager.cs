using TMPro;
using UnityEngine;
using Settings.Input;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public InputReader inputReader;
    public StarSpawner starSpawner;

    private enum TutorialStep
    {
        Move,
        Shoot,
        Stars,
        Meteors,
        Complete
    }

    private TutorialStep currentStep;

    void Start()
    {
        currentStep = TutorialStep.Move;
        tutorialText.text = "Move with A / D";

        // Disable spawning until tutorial progresses
        starSpawner.enabled = false;
    }

    void OnEnable()
    {
        inputReader.MoveEvent += OnMove;
        inputReader.ShootStartedEvent += OnShoot;
    }

    void OnDisable()
    {
        inputReader.MoveEvent -= OnMove;
        inputReader.ShootStartedEvent -= OnShoot;
    }

    void OnMove(Vector2 moveDir)
    {
        if (currentStep != TutorialStep.Move) return;

        if (Mathf.Abs(moveDir.x) > 0)
        {
            currentStep = TutorialStep.Shoot;
            tutorialText.text = "Hold SPACE to Shoot";
        }
    }

    void OnShoot()
    {
        if (currentStep != TutorialStep.Shoot) return;

        currentStep = TutorialStep.Stars;
        tutorialText.text = "Collect the Falling Stars!";

        StartStars();
    }

    void StartStars()
    {
        starSpawner.enabled = true;
        starSpawner.fallingStarToMeteorRatio = 1f; // stars only

        Invoke(nameof(StartMeteors), 6f);
    }

    void StartMeteors()
    {
        currentStep = TutorialStep.Meteors;
        tutorialText.text = "Destroy Meteors with your spells!";

        starSpawner.fallingStarToMeteorRatio = 0.5f;
    }
}