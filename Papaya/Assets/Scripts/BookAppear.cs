using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAppear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PapayaEvents.Register("OnDialogueEnd", OnDialogueEnd);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDialogueEnd(Dialogue d)
    {
        Debug.Log("Got the message " + d.name);
        gameObject.SetActive(true);
    }
}
