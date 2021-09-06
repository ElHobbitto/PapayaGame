using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public float frequency = 1.0f;
    public float speed = 1.0f;
    public GameObject myObject;
    //public bool gameStarted = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke ("SpawnObject",frequency);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObject() 
    {
        //if(gameStarted == true)
        {
            Instantiate(myObject, transform.position,Quaternion.identity);
         }
          Invoke ("SpawnObject", Random.value *frequency);
    }
}
