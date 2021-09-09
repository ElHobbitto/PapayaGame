using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MinigameHighscore
{
    public string name;
    public int score;
}


[CreateAssetMenu(fileName="New Highscores Data", menuName = "Highscore Data")]
[System.Serializable]
public class MinigameHighScores : ScriptableObject
{
    public int numScores = 10;
    public MinigameHighscore[] inspectorScores;
    LinkedList<MinigameHighscore> scores;
    [SerializeField]
    static public int lastScore;

    void OnEnable()
    {
        Debug.Log("Minigame Save Data OnEnabled called.");
        //deserialise the scores?
        if (scores == null)
        {    
            scores = new LinkedList<MinigameHighscore>();
        
            if (inspectorScores != null)
            {
                foreach (MinigameHighscore inspectorscore in inspectorScores)
                {
                    if (inspectorscore.score > 0)
                    {
                        AddScore(inspectorscore.name, inspectorscore.score);
                    }
                }
            }
            else
            {
                inspectorScores = new MinigameHighscore[numScores];
            }
        }
    }

    public void Save()
    {

    }

    public bool IsScoreHighEnough(int score)
    {
        Debug.Log("Checking if score is high enough: " + score);
        bool retval = false;
        if (scores!= null)
        {
            if (scores.Count < numScores)
            {
                Debug.Log("Score will be high enough because there aren't enough scores");
                retval = true;
            }
            else if (scores.Count > 0)
            {
                Debug.Log("Need to check for lowest score");
                MinigameHighscore lowestScore = FindLowestScore();
                Debug.Log("Lowest score was " + lowestScore.score);
                if (lowestScore != null)
                {
                    if (score >= lowestScore.score)
                    {
                        retval = true;
                    }
                }
            }
        }
        return retval;
    }

    public bool AddScore(string name, int score)
    {
        bool retval = false;
        MinigameHighscore thescore = new MinigameHighscore();
        thescore.name = name;
        thescore.score = score;

        if (scores != null)
        {
            if (scores.Count == 0)
            {
                Debug.Log("Adding score as first score");
                scores.AddFirst(thescore);
                retval = true;
            }
            else
            {
                LinkedListNode<MinigameHighscore> before = FindScoreBefore(score);
                if (before != null)
                {
                    Debug.Log("Adding score after " + before);
                    scores.AddAfter(before, thescore);
                    retval = true;
                }
                else
                {
                    //Debug.Log("AddScore Error: Before score was null");
                    //If before was null, (and scores was ok and count > 0)
                    //then there is no before - we must be the highest score.
                    scores.AddFirst(thescore);
                }
            }
        }
        else
        {
            Debug.Log("AddScore Error: scores was null");
        }
        while (scores.Count > numScores)
        {
            scores.RemoveLast();
        }

        //inspectorScores = new MinigameHighscore[numScores];
        scores.CopyTo(inspectorScores, 0);

        return retval;
    }

    public MinigameHighscore[] GetScoresArray()
    {
        //MinigameHighscore[] scoresarray = new MinigameHighscore[numScores];
        //scores.CopyTo(scoresarray, 0);
        //return scoresarray;
        return inspectorScores;
    }

    MinigameHighscore FindLowestScore()
    {
        MinigameHighscore retval = null;
        //int lowestScore;
        if (scores != null && scores.Count > 0)
        {
            LinkedListNode<MinigameHighscore> node = scores.First;
            retval = node.Value;
            int lowestScore = retval.score;
            while (node.Next != null)
            {
                if (node.Value.score < lowestScore)
                {
                    retval = node.Value;
                    lowestScore = retval.score;
                }
                node = node.Next;
            }
        }
        return retval;
    }

    //Find the score just higher than or equal to this score
    LinkedListNode<MinigameHighscore> FindScoreBefore(int score)
    {
        Debug.Log("Searching for score before " + score);
        Debug.Log("Scores: " + scores);
        if (scores != null)
            Debug.Log("Scores.Count: " + scores.Count);
        LinkedListNode<MinigameHighscore> retval = null;
        if (scores != null && scores.Count > 0)
        {
            LinkedListNode<MinigameHighscore> node = scores.First;
            while (node != null)
            {
                if (node.Value.score >= score)
                {
                    if (retval != null)
                    {
                        if (node.Value.score <= retval.Value.score)
                        {
                            Debug.Log("Found closer score: " + node.Value.score);
                            retval = node;
                        }
                    }
                    else
                    {
                        Debug.Log("Setting before to initial value of " + node.Value.score);
                        retval = node;
                    }
                }
                node = node.Next;
            }

        }
        else
        {
            Debug.Log("Scores was null or count was 0");
        }
        return retval;
    }

}
