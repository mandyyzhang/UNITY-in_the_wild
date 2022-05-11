using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "Wake Up Scene") {
            Debug.Log("Clicked screen to go to island 1 (starting game)."); 
            SceneManager.LoadScene(1); 
        }
    }
}
