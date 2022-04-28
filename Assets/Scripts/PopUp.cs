using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{

    private Canvas canvasPopUp; 
    
    void Awake()
    {
        canvasPopUp = GetComponent<Canvas>(); 
        canvasPopUp.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !canvasPopUp.enabled)
		{
			canvasPopUp.enabled = true;
		} else if (Input.GetKeyDown(KeyCode.P) && canvasPopUp.enabled)
        {
            canvasPopUp.enabled = false;
        }
    }
}
