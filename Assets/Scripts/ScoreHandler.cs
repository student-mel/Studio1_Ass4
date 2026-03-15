using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    private int score;
    [Tooltip("Use this to define what a player's target score for the stage is")]  
    public int targetScore = 25; //25 as default value

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider scoreBar;
    [SerializeField] private TextMeshProUGUI winGameOverText;

    void Awake()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>(); // Find the TextMeshProUGUI component in the scene
        }
        if (scoreBar == null)
        {
            scoreBar = GameObject.Find("ScoreBar").GetComponent<Slider>(); // Find the Slider component in the scene
        }
        if (winGameOverText == null)
        {
            winGameOverText = GameObject.Find("WinGameOverText").GetComponent<TextMeshProUGUI>(); // Find the TextMeshProUGUI component for win/game over text in the scene
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        scoreText.color = Color.purple; //set the text color to something that's visible against the bar
        winGameOverText.enabled = false; // Hide the win/game over text initially
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score.ToString() + "/" + targetScore.ToString();
        scoreBar.value = (float)score / targetScore; // Update the scrollbar value based on the current score and target score

        if (score >= targetScore)
        {
            winGameOverText.enabled = true; // Show the win/game over text
            winGameOverText.text = "Level Complete!";

            FindObjectOfType<LevelProgressionManager>().TriggerLevelComplete();
            Time.timeScale = 0f; // Pause the game
        }
    }

    public int GetScore()
    {
        return score; // For score level requirement
    }

    public void AddScore(int Addingscore)
    {
        score += Addingscore;
        if (score < 0)
        {
            score = 0; // Ensure the score doesn't go below 0
        }
    }
}
