using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winGameOverText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button exitButton;

    public bool isPaused;

    void Awake()
    {
        if (winGameOverText == null)
        {
            winGameOverText = GameObject.Find("WinGameOverText").GetComponent<TextMeshProUGUI>(); // Find the TextMeshProUGUI component for win/game over text in the scene
        }
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

        // Add listeners to the buttons
        retryButton.onClick.AddListener(RetryButtonClicked);
        menuButton.onClick.AddListener(MenuButtonClicked);
        exitButton.onClick.AddListener(QuitButtonClicked);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f; //force it back to life if it gets stuck
        isPaused = false;
        winGameOverText.enabled = false; // Hide the win/game over text initially
        retryButton.gameObject.SetActive(false); // Disable the retry button
        menuButton.gameObject.SetActive(false); // Disable the menu button
        exitButton.gameObject.SetActive(false); // Disable the exit button
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
           if (winGameOverText.enabled && !isPaused)
            {
                return; // If the win/game over text is already enabled and the game is not paused, ignore the pause input to prevent conflicts from having won/lost the game already
            }
           if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
        winGameOverText.enabled = true; // Show the pause text
        winGameOverText.text = "Game Paused";
        retryButton.gameObject.SetActive(true); // Enable the retry button
        menuButton.gameObject.SetActive(true); // Enable the menu button
        exitButton.gameObject.SetActive(true); // Enable the exit button
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
        winGameOverText.enabled = false; // Hide the pause text
        retryButton.gameObject.SetActive(false); // Disable the retry button
        menuButton.gameObject.SetActive(false); // Disable the menu button
        exitButton.gameObject.SetActive(false); // Disable the exit button
    }

    public void RetryButtonClicked()
    {
        Time.timeScale = 1f; // Ensure the game is unpaused when retrying
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void MenuButtonClicked()
    {
        Time.timeScale = 1f; // Ensure the game is unpaused when returning to the main menu
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Load the main menu scene (assuming it's at index 0)
    }

    public void QuitButtonClicked()
    {
        Application.Quit();

        #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
