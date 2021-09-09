using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    Dialogue currentDialogue;
    public Expressions expressionManager;
  
    

    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue (Dialogue dialogue)
    {
        currentDialogue = dialogue;
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence ()
    {
        if(sentences.Count == 0)
        {
            
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();

        //split out any expression markup
        string[] bits = sentence.Split('|');
        if (bits.Length >= 2)
        {
            for (int i = 0; i < bits.Length-1; i++)
            {
                //expressions is bits[0]
                string[] expressions = bits[i].Split(':');
                string charname = expressions[0].Trim();
                string expressionname = expressions[1].Trim();
                if (expressionManager!=null)
                {
                    expressionManager.SetCharacterExpression(charname, expressionname);
                }
            }
            
            sentence = bits[bits.Length-1];
        }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    void EndDialogue()
    {
        //animator.SetBool("IsOpen", false);
        //animator.Play("achievement");
        //Bookopen = true;
        //animator.Play("on");
        
        Debug.Log("book");
        PapayaEvents.Fire("OnDialogueEnd", currentDialogue);
    }

    // Update is called once per frame
   
}
