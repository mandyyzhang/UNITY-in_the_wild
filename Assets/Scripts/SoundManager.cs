using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    // Need to access variables in first person controller 
    private FirstPersonController fpc; 
    private SelectionManager selectScript; 
    [SerializeField] private GameObject player; 
    [SerializeField] private GameObject selectionManager; 

    // variables from first e

    public AudioSource movementSound;
    public AudioSource jumpSound;
    public AudioSource collectGlassSound;
    public AudioSource treeShakeSound;
    private float _timeSinceLastStepPlayed;

    void Awake()
    {
        fpc = player.GetComponent<FirstPersonController>(); 
        selectScript = selectionManager.GetComponent<SelectionManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        #region Player Sound Effects 

        // jump sound effects: 
        if (fpc.jumping) {
            jumpSound.Play(); 
            fpc.jumping = false; 
        }

        #region footsteps sound effect 
        if (fpc.walking && fpc.grounded)
        {
            _timeSinceLastStepPlayed += Time.deltaTime;
            if (fpc.sprinting) { // if sprinting speed up footsteps sound effect 
                if (_timeSinceLastStepPlayed > 0.2) {
                    _timeSinceLastStepPlayed = 0; 
                    movementSound.Play(); 
                }
            } 
            else { // if we are just walking use slower sound effect 
                
                if (_timeSinceLastStepPlayed > 0.4) {
                    _timeSinceLastStepPlayed = 0;
                    movementSound.Play();
                }
            }
        }
        #endregion 
        #endregion 

        #region Game System Sound Effects

        if (selectScript.obtainedItem) {
            collectGlassSound.Play();
            selectScript.obtainedItem = false; 
        } else if (selectScript.treeShake) {
            treeShakeSound.Play();
            selectScript.treeShake = false;
        }
            

        #endregion

    }
}
