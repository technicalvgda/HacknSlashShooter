using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the tracking of scores. Uses the save handler to record scores.
/// </summary>
public class ScoreHandler : MonoBehaviour {
    // Singleton
    public static ScoreHandler s;

    // Amount of score the player currently has
    public int currentScore { get; private set; }

    /// <summary>
    /// Initializes the singleton and the currentScore
    /// </summary>
	void Start () {
        s = this;
        currentScore = 0;
	}

    /// <summary>
    /// Adds the current score to the high scores player pref if it is high enough
    /// </summary>
    /// <returns>If the current score is high enough</returns>
    public bool RecordScore()
    {
        var scoreList = SaveHandler.s.GetScores();
        var isNewHighScore = SaveHandler.s.IsNewHighScore(currentScore, scoreList);

        scoreList.Add(currentScore);
        SaveHandler.s.RecordScores(scoreList);
        currentScore = 0;

        return isNewHighScore;
    }

    /// <summary>
    /// Adds an integer to the current score
    /// </summary>
    /// <param name="score">Amount to add to current score</param>
    public void AddScore(int score)
    {
        currentScore += score;
    }
}
