using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movescript : MonoBehaviour

{
    public float moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed,0,0);  
        
    }
}
