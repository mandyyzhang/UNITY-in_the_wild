using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    //Referencing other scripts here: 
    private DialogueManager dm; 
    [SerializeField] private GameObject dialogueManager; 

    void Awake() {
        dm = dialogueManager.GetComponent<DialogueManager>(); 
    }

    public void TriggerDialogue()
    {
        // FindObjectOfType<DialogueManager>().StartDialogue();
        dm.StartDialogue(); 
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            // FindObjectOfType<DialogueManager>().DisplayNextSentence();
            dm.DisplayNextSentence(); 
        }
    }
}
