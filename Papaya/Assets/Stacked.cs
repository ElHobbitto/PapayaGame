using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stacked : MonoBehaviour
{
  public Animator anim;
   
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CHEF") )
        {
           anim.Play("static");
        }

    

    }
    
    }
