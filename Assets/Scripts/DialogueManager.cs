using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//PUT DIALOGUE TRIGGER SCRIPT ON THE OBJECT YOU WANT TO HAVE DIALOGUE

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public GameObject dialogueBox; 
    private Queue<string> sentences;

    public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
        sentences = new Queue<string>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue()
    {
        dialogueBox.SetActive(true);
        Debug.Log("starting convo");

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("end of convo");
        dialogueBox.SetActive(false);
    }


}
