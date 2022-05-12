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

    // Reference the first person controller here 
    // & make it so that when there is dialogue there is no player movement
    private FirstPersonController fpc; 
    [SerializeField] private GameObject firstPersonController; 

    void Awake() {
        fpc = firstPersonController.GetComponent<FirstPersonController>(); 
        dialogueBox.SetActive(false);
    }

    void Start() {
        sentences = new Queue<string>();
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.Return))
        if (Input.GetMouseButton(1)) // mouse right click to continue
        {
            DisplayNextSentence();
        }
    }

    public void displayDialogue(int index)
    {
        fpc.pauseControls = true; 
        dialogueBox.SetActive(true);

        sentences.Clear();
        dialogueText.text = dialogue.sentences[index];

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
        if (sentences.Count <= 0)
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
        fpc.pauseControls = false; 
    }


}
