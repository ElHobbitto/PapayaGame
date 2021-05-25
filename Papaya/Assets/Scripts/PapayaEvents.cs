using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapayaEvents : MonoBehaviour
{
    public delegate void DialogueEventAction(Dialogue dialogue);

    Dictionary<string, List<DialogueEventAction>> events;
    // Start is called before the first frame update
    static PapayaEvents currentPapayaEvents;

    void Awake()
    {
        currentPapayaEvents = this;
        events = new Dictionary<string, List<DialogueEventAction>>(); 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Register(string eventname, DialogueEventAction action)
    {
        //if no static current event variable, exit 
        if (currentPapayaEvents == null)
            return;
        //create the key and list if it doesn't exist
        if (! currentPapayaEvents.events.ContainsKey(eventname))
        {
            currentPapayaEvents.events[eventname] = new List<DialogueEventAction>();
        }
        //only add the action if it's not in the list
        if (!currentPapayaEvents.events[eventname].Contains(action))
        {   
            currentPapayaEvents.events[eventname].Add(action);
        }
    }

    public static void Fire(string eventname, Dialogue dialogue)
    {
        if (currentPapayaEvents == null)
            return;
        if (!currentPapayaEvents.events.ContainsKey(eventname))
            return;
        foreach (DialogueEventAction dea in currentPapayaEvents.events[eventname])
        {
            dea.Invoke(dialogue);
        }
    }



}
