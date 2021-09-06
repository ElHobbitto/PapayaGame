using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stacked : MonoBehaviour
{
  
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
     if(other.gameObject)
        {
            //Score.ScoreValue += 10;
            //Destroy(this.gameObject);
            //other.gameObject.transform.SetParent(gameObject.transform);
            //Debug.Log("PENG");
            

        }
    }
        
    
    }
