using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem; // Required for new Input System

public class LevelProgressionManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI continueText;

    private bool levelComplete = false;

    void Start()
    {
        if (continueText != null)
        {
            continueText.enabled = false;
        }
    }

    void Update()
    {
        if (!levelComplete) return;

        // Use the new Input System to detect ENTER / Return
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
    }

    public void TriggerLevelComplete()
    {
        levelComplete = true;

        if (continueText != null)
        {
            continueText.enabled = true;
            continueText.text = "Press ENTER to continue";
        }
    }

    void LoadNextLevel()
    {
        Time.timeScale = 1f;

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }
}