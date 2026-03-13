using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public int score;
    [Tooltip("Use this to define what a player's target score for the stage is")]  
    public int targetScore = 25; //25 as default value

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
