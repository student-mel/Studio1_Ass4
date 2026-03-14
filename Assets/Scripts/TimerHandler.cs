using UnityEngine;
using TMPro;

public class TimerHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI timerText; // Reference to your UI Text element
    [Tooltip("Use this to define what a player's time limit for the stage is")]
    public int timeLimit = 60; // Time limit in seconds - keep this public so that it can be easily adjusted in the Unity editor
    private float timeRemaining; // Variable to track the remaining time
    private bool timerIsRunning = false; // Flag to check if the timer is running

    [SerializeField] private TextMeshProUGUI winGameOverText;
    void Awake()
    {
        if (timerText == null)
        {
            timerText = GameObject.Find("timerText").GetComponent<TextMeshProUGUI>(); // Find the TextMeshProUGUI component in the scene
        }
        if (winGameOverText == null)
        {
            winGameOverText = GameObject.Find("WinGameOverText").GetComponent<TextMeshProUGUI>(); // Find the TextMeshProUGUI component for win/game over text in the scene
        }
    }
    void Start()
    {
        timeRemaining = (float)timeLimit; // Initialize the remaining time to the time limit 
        timerIsRunning = true; // Start the timer
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            timeRemaining -= Time.deltaTime; // Decrease the remaining time by the time that has passed since the last frame
            DisplayTime(timeRemaining); // Update the timer display
        }
        else
        {
            winGameOverText.enabled = true; // Show the win/game over text
            winGameOverText.text = "Time Out - Game Over!";
            Time.timeScale = 0f; // Pause the game
        }
    }

    void DisplayTime(float displayTime)
    {
        if (displayTime <= 0)
        {
            displayTime = 0;
            timerIsRunning = false; // Stop the timer when it reaches 0
        }
        float minutes = Mathf.FloorToInt(displayTime / 60); // Calculate the minutes
        float seconds = Mathf.FloorToInt(displayTime % 60); // Calculate the seconds

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Update the timer text in the format MM:SS
    }
}
