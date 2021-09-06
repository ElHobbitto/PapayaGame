using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectappear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.CompareTag("X");
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
