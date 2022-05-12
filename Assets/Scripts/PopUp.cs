using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{

    private Canvas canvasPopUp; 
    private CanvasGroup panel;
    private bool fadeIn = false; 
    private bool fadeOut = false;  
    
    private FirstPersonController fpc; 
    [SerializeField] private GameObject firstPersonController; 

    // toggle enableDebug to false to get rid of press P to show next island preview 
    public bool enableDebug = true; 
    
    void Awake()
    {
        fpc = firstPersonController.GetComponent<FirstPersonController>(); 
        canvasPopUp = GetComponent<Canvas>(); 
        panel = GetComponent<CanvasGroup>(); 
        panel.alpha = 0; 
        canvasPopUp.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {

        if (enableDebug) {

            if (Input.GetKeyDown(KeyCode.P) && !canvasPopUp.enabled) {    
                canvasPopUp.enabled = true;
                fpc.pauseControls = true; 
                fadeIn = true; 
            } 
            else if (Input.GetKeyDown(KeyCode.P) && canvasPopUp.enabled) {
                fadeOut = true; 
            }

            if (fadeIn) {
                if (panel.alpha >= 1) {
                    fadeIn = false; 
                }
                panel.alpha += Time.deltaTime;  
            }

            if (fadeOut) {
                if (panel.alpha <= 0) {
                    fadeOut = false; 
                    canvasPopUp.enabled = false; 
                    fpc.pauseControls = false; 
                    //debug comments 
                    /*
                    Debug.Log("fpc.pauseControls = " + fpc.pauseControls);
                    Debug.Log("fpc.playerCanMove = " + fpc.playerCanMove); 
                    Debug.Log("fpc.cameraCanMove = " + fpc.cameraCanMove);
                    Debug.Log("fpc.enableJump = " + fpc.enableJump);
                    */ 
                }
                panel.alpha -= Time.deltaTime; 
            }

        } // end of enableDebug = true

    } // end of Update() 

}
