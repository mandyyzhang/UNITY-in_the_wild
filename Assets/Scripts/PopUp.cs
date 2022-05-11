using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{

    private Canvas canvasPopUp; 
    private CanvasGroup panel;
    private bool fadeIn = false; 
    private bool fadeOut = false;  
    
    void Awake()
    {
        canvasPopUp = GetComponent<Canvas>(); 
        panel = GetComponent<CanvasGroup>(); 
        panel.alpha = 0; 
        canvasPopUp.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !canvasPopUp.enabled) {    
            canvasPopUp.enabled = true;
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
            }
            panel.alpha -= Time.deltaTime; 
        }

    }

}
