using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles arena high scores and also records whether or not the player has cleared
/// the game (through the absense of a high score file)
/// </summary>
public class SaveHandler : MonoBehaviour
{
    // Enables controls to test if the savehandler works
    public bool DebugMode;

    // Singleton
    public static SaveHandler s;

    // Set to true if a highscores playerpref exists
    public bool gameFinished
    {
        get; private set;
    }

    const string SAVE_KEY = "highscores";

    const int HIGH_SCORE_LIMIT = 10;

    /// <summary>
    /// Initializes both the singleton and gameFinished property
    /// </summary>
    void Start () {
        s = this;
        var currentScores = PlayerPrefs.GetString(SAVE_KEY,"");

        if (currentScores.Equals(""))
        {
            gameFinished = false;
        }
        else
        {
            gameFinished = true;
        }

        // Validates scores
        GetScores();
	}

    /// <summary>
    /// Iterates through the score list and checks if the input is greater than any
    /// of the scores in the list.
    /// </summary>
    /// <param name="score">Input to check against the score list</param>
    /// <returns>If the input is a new high score</returns>
    public bool IsNewHighScore(int score, IList scoreList)
    {
        var result = false;

        for(int i = 0; i < scoreList.Count; i++)
        {
            if (score > (int)scoreList[i])
            {
                result = true;
            }
        }

        return result;
    }

    /// <summary>
    /// Parses and saves the list into playerprefs
    /// </summary>
    /// <param name="scoreList">The list to save into player prefs</param>
    /// <returns>If saving the list is successful</returns>
    public bool RecordScores(IList scoreList)
    {
        if (scoreList.Count < HIGH_SCORE_LIMIT)
            return false;

        var scores = "";
        var scoreListClone = new List<int>((IEnumerable<int>)scoreList);


        // Saves the top N high scores.
        scoreListClone.Sort();
        scoreListClone.Reverse();

        for(int i = 0; i < HIGH_SCORE_LIMIT; i++)
        {
            scores += scoreListClone[i];
            if (i < HIGH_SCORE_LIMIT-1)
            {
                scores += " ";
            }
        }

        PlayerPrefs.SetString(SAVE_KEY, scores);
        PlayerPrefs.Save();

        return true;
    }

    /// <summary>
    /// Parses the currentScore string into a List.
    /// </summary>
    /// <returns>List of high scores</returns>
    public IList GetScores()
    {
        var scoreList = new List<int>();
        var currentScores = PlayerPrefs.GetString(SAVE_KEY, "").Split(' ');
        var score = 0;

        for(int i = 0; i < currentScores.Length; i++)
        {
            if (int.TryParse(currentScores[i], out score))
            {
                scoreList.Add(score);
            }
            else
            {
                gameFinished = false;
                return null;
            }
        }

        return scoreList;
    }

    /// <summary>
    /// Creates a list full of 0's and calls RecordScore on that list
    /// </summary>
    public void InitializeScores()
    {
        var scoreList = new List<int>();

        for(int i = 0; i < HIGH_SCORE_LIMIT; i++)
        {
            scoreList.Add(0);
        }
        gameFinished = true;

        RecordScores(scoreList);
    }

    /// <summary>
    /// Implements controls to test the handler.
    /// SPACE - Check and see if playerprefs are saved
    /// Q - Alternates between instantiating and deleting the playerprefs
    /// </summary>
    void Update()
    {
        if (DebugMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gameFinished)
                {
                    Debug.Log("Game is finished.");
                }
                else
                {
                    Debug.Log("Game is not finished.");
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (gameFinished)
                {
                    PlayerPrefs.DeleteKey(SAVE_KEY);
                    PlayerPrefs.Save();
                    GetScores();
                    Debug.Log("Deleted");
                }
                else
                {
                    InitializeScores();
                    Debug.Log("Initialized Scores");
                }
            }
        }
    }
}