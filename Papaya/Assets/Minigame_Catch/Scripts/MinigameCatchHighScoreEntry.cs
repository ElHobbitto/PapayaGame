using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MinigameCatchHighScoreEntry : MonoBehaviour
{
    public MinigameHighScores highscoreData;
    public Text scoreText;
    public GameObject highScoreRoot;
    public InputField nameinput;

    string hs_name;
    int hs_score;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("High score scene is reading " + highscoreData.lastScore + " out of score data");
        Init(MinigameHighScores.lastScore); //get the last score off the highscore data.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int score)
    {
        hs_score = score;
        highScoreRoot.SetActive(false);
        scoreText.text = "Score: " + score;
        if (highscoreData.IsScoreHighEnough(score))
        {
            highScoreRoot.SetActive(true);
            nameinput.Select();
        }
    }

    public void OnNameChange(string n)
    {
        hs_name = n;
    }

    public void OnSubmit()
    {
        highscoreData.AddScore(hs_name, hs_score);
    }
}
