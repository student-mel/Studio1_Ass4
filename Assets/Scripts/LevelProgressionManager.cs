using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelProgressionManager : MonoBehaviour
{
    [Header("References")]
    public ScoreHandler scoreHandler;
    public TextMeshProUGUI continueText;

    private bool levelComplete = false;

    void Start()
    {
        if (scoreHandler == null)
        {
            scoreHandler = GameObject.Find("Canvas").GetComponent<ScoreHandler>();
        }

        if (continueText != null)
        {
            continueText.enabled = false;
        }
    }

    void Update()
    {
        if (!levelComplete && scoreHandler.GetScore() >= scoreHandler.targetScore)
        {
            LevelComplete();
        }

        if (levelComplete && Input.GetKeyDown(KeyCode.Return))
        {
            LoadNextLevel();
        }
    }

    void LevelComplete()
    {
        levelComplete = true;

        if (continueText != null)
        {
            continueText.enabled = true;
            continueText.text = "Press ENTER to continue";
        }

        Time.timeScale = 0f;
    }

    void LoadNextLevel()
    {
        Time.timeScale = 1f;

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }
}