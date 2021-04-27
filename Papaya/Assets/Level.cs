using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    // Start is called before the first frame update
    void LoadScene()
    {
       SceneManager.LoadScene("Dialogue animated"); 
    }

    // Update is called once per frame
   
}
