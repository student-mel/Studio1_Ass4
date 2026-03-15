using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    [Header("References")]
    public StarSpawner starSpawner;
    public ShootingStarSpawner shootingStarSpawner;
    public ScoreHandler scoreHandler;

    [Header("Difficulty Scaling")]
    public float speedIncreasePerLevel = 0.5f;
    public int scoreIncreasePerLevel = 10;

    void Start()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;

        ApplyDifficulty(levelIndex);
    }

    void ApplyDifficulty(int level)
    {
        if (starSpawner != null)
        {
            starSpawner.fallingStarSpeed += level * speedIncreasePerLevel;
            starSpawner.meteorSpeed += level * speedIncreasePerLevel;
        }

        if (shootingStarSpawner != null)
        {
            shootingStarSpawner.shootingStarSpeed += level * speedIncreasePerLevel;
        }

        if (scoreHandler != null)
        {
            scoreHandler.targetScore += level * scoreIncreasePerLevel;
        }
    }
}