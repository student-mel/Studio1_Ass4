using TMPro;
using UnityEngine;
using Settings.Input;

public class TutorialManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI tutorialText;

    [Header("Input")]
    public InputReader inputReader;

    [Header("Spawners")]
    public StarSpawner starSpawner;
    public ShootingStarSpawner shootingStarSpawner;

    [Header("Spell System")]
    public SpellsManager spellsManager;

    private enum TutorialStep
    {
        Move,
        Shoot,
        Stars,
        Meteors,
        ShootingStars,
        MagnetSpell,
        Complete
    }

    private TutorialStep currentStep;

    void Start()
    {
        currentStep = TutorialStep.Move;
        tutorialText.text = "Move with A / D";

        starSpawner.enabled = false;
        shootingStarSpawner.enabled = false;
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

    void OnMove(Vector2 dir)
    {
        if (currentStep != TutorialStep.Move) return;

        if (Mathf.Abs(dir.x) > 0)
        {
            currentStep = TutorialStep.Shoot;
            tutorialText.text = "Hold SPACE to Shoot";
        }
    }

    void OnShoot()
    {
        if (currentStep != TutorialStep.Shoot) return;

        currentStep = TutorialStep.Stars;
        tutorialText.text = "Collect the Falling Stars";

        StartStars();
    }

    void StartStars()
    {
        starSpawner.enabled = true;
        starSpawner.fallingStarToMeteorRatio = 1f;

        Invoke(nameof(StartMeteors), 8f);
    }

    void StartMeteors()
    {
        currentStep = TutorialStep.Meteors;
        tutorialText.text = "Destroy Meteors with your spells";

        starSpawner.fallingStarToMeteorRatio = 0.4f;

        Invoke(nameof(StartShootingStars), 8f);
    }

    void StartShootingStars()
    {
        currentStep = TutorialStep.ShootingStars;
        tutorialText.text = "Hit the Shooting Stars for bonus points";

        shootingStarSpawner.enabled = true;

        Invoke(nameof(StartMagnetSpell), 10f);
    }

    void StartMagnetSpell()
    {
        currentStep = TutorialStep.MagnetSpell;
        tutorialText.text = "Destroy meteors to earn a Magnet Spell";

        // Lower threshold so tutorial triggers faster
        foreach (var spell in spellsManager.spells)
        {
            if (spell.spellEffect == SpellStats.SpellEffect.Magnet)
            {
                spell.meteorThreshold = 2;
            }
        }

        Invoke(nameof(EndTutorial), 15f);
    }

    void EndTutorial()
    {
        tutorialText.text = "Press # to return to the main menu";
        currentStep = TutorialStep.Complete;
    }
}