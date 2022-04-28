using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneChanger : MonoBehaviour
{
    public KeyCode restart = KeyCode.R; 
    public KeyCode island1 = KeyCode.Alpha1; 
    public KeyCode island2 = KeyCode.Alpha2; 



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(restart))
        {
            Debug.Log("Pressed R key to restart.");
            Scene curr_scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(curr_scene.name);
        }

        if (Input.GetKeyDown(island1)) {
            Debug.Log("Pressed 1, to switch to island 1.");
            SceneManager.LoadScene(0); 
        }

        if (Input.GetKeyDown(island2)) {
            Debug.Log("Pressed 2, to switch to island 2."); 
            SceneManager.LoadScene(1); 
        }
    }
}
