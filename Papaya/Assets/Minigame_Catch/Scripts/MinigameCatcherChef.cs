using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MinigameCatcherChef : MonoBehaviour
{
    public int score = 0;
    public float moveSpeed = 1f;
    public float plateUpwardsMovement = 0.2f;
    public GameObject plate;
    public IngredientsSpawner spawner;
    public Text scoreText;
    public string sceneToLoadOnLose = "Game over";
    public MinigameHighScores highScores;
    public int maxInStack = 5;
    int inStack = 0; 
    Vector3 plateInitialLocalPosition;
    GameObject[] stackObjects;
    public GameObject bonusPrefab;
    // Start is called before the first frame update
    void Start()
    {
        AddScore(0);
        plateInitialLocalPosition = plate.transform.localPosition;
        stackObjects = new GameObject[maxInStack];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.right * -moveSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * moveSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Good food"))
        {
            Stack(other.gameObject);
            AddScore(spawner.gameParams.pointsPerFood);
        }
        else if (other.gameObject.CompareTag("Bad food"))
        {
            SceneManager.LoadScene(sceneToLoadOnLose);
            //Debug.Log("Oldscene reading  " + highScores.lastScore + " out of score data");
            
        }
    }

    void Stack(GameObject g)
    {
        g.transform.SetParent(transform); //parent to us
        //Snap position to plate
        g.transform.position = plate.transform.position;
        //move plate hitbox up
        plate.transform.position += Vector3.up * plateUpwardsMovement;
        //move papaya down
        //transform.position -= Vector3.up * plateUpwardsMovement;
        g.GetComponent<Move>().enabled = false; //no more movement
        g.GetComponent<Collider2D>().enabled = false; //no collider on caught food

        stackObjects[inStack] = g;
        inStack+=1;
        if (inStack == maxInStack)
        {
            ResetStack();
        }

    }

    void ResetStack()
    {
        if (bonusPrefab != null)
        {
            Instantiate(bonusPrefab, stackObjects[stackObjects.Length-1].transform.position, Quaternion.identity);
        }
        foreach (GameObject g in stackObjects)
        {
            if (g != null)
            {
                Destroy(g);
            }
        }
        plate.transform.localPosition = plateInitialLocalPosition;
        AddScore(100);
        inStack = 0;
        
    }

    void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
        spawner.AdviseScore(score); //tell the spawner the score
        MinigameHighScores.lastScore = score;
        
    }
}
