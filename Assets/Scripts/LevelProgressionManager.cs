using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem; // Required for new Input System
using UnityEngine.UI;

public class LevelProgressionManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI continueText;

    private bool levelComplete = false;

    [Header("Pause Menu Buttons")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button exitButton;

    void Awake()
    {
        if (retryButton == null)
        {
            retryButton = GameObject.Find("retryButton").GetComponent<Button>(); // Find the Button component for the retry button in the scene
        }
        if (menuButton == null)
        {
            menuButton = GameObject.Find("menuButton").GetComponent<Button>(); // Find the Button component for the retry button in the scene
        }
        if (exitButton == null)
        {
            exitButton = GameObject.Find("exitButton").GetComponent<Button>(); // Find the Button component for the retry button in the scene
        }
    }
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
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings-1)
        {
            continueText.text = "Game Complete";
            retryButton.gameObject.SetActive(true); // Enable the retry button
            menuButton.gameObject.SetActive(true); // Enable the menu button
            exitButton.gameObject.SetActive(true); // Enable the exit button
        }
    }

    void LoadNextLevel()
    {
        Time.timeScale = 1f;

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }
}