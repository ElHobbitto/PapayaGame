using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public int score = 0;

    public float moveSpeed = 1f;
    public float plateUpwardsMovement = 0.2f;
    public GameObject plate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Horizontal"))
        transform.Translate(-moveSpeed,0,0);
        if(Input.GetButtonDown("Vertical"))
        transform.Translate(moveSpeed,0,0);
        if (score == 1000 )
        {
            SceneManager.LoadScene("win LEVEL 1"); 
             
        }
        if (score ==2500)
        {

            SceneManager.LoadScene("win LEVEL 2");
        }
        if (score >= 4000)
        {
            SceneManager.LoadScene("win LEVEL 3");
        }
    
    }
  
    
  
    void OnTriggerEnter2D(Collider2D other)
    {
         if(other.gameObject.CompareTag("Good food"))
        {
            other.gameObject.transform.SetParent(transform); //parent to us.
            //snap the food positin to the plate
            Vector3 foodpos = other.transform.position;
            foodpos.x = plate.transform.position.x;
            foodpos.y = plate.transform.position.y;
            
            other.transform.position = foodpos;

            //move the plate hitbox up.
            plate.transform.position += (Vector3)(Vector2.up * plateUpwardsMovement);

            //Score.ScoreValue += 10;
            other.gameObject.GetComponent<Move>().enabled = false;
            Text myScoreText = GameObject.Find("Score").GetComponent<Text>();
            score += 100;
            myScoreText.text = "Score:" + score.ToString();
            Debug.Log("Peng");

        }
        if( other.gameObject.CompareTag("Bad food"))
        {
            SceneManager.LoadScene("Game over"); 
        }
    }

}
