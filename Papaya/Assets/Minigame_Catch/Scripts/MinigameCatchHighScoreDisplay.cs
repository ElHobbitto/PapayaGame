using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameCatchHighScoreDisplay : MonoBehaviour
{
    public Text highscoreText;
    public MinigameHighScores highScoreData;
    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        highscoreText.text = "";
        MinigameHighscore[] scores = highScoreData.GetScoresArray();
        foreach (MinigameHighscore hs in scores)
        {
            if (hs != null)
            {
                string hstext = "" + (index+1) + ": " + hs.name + " - " + hs.score;
                if (index <= 2)
                {
                    highscoreText.text += "<b>" + hstext + "</b>" + "\n";
                }
                else
                {
                    highscoreText.text += hstext + "\n";
                }
                index += 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
