using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneChanger : MonoBehaviour
{
    public KeyCode restart = KeyCode.R; 
    public KeyCode island1 = KeyCode.Alpha1; 
    public KeyCode island2 = KeyCode.Alpha2; 

    private Scene curr_scene; 

    // Note: to see what index the scene is at, look at the build settings 
    void Awake() {
        curr_scene = SceneManager.GetActiveScene(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (curr_scene.name == "Load" || curr_scene.buildIndex == 0) {
            return; 
        }
        
        if (Input.GetKeyDown(restart))
        {
            Debug.Log("Pressed R key to restart.");
            SceneManager.LoadScene(curr_scene.name);
        }

        if (Input.GetKeyDown(island1)) {
            Debug.Log("Pressed 1, to switch to island 1.");
            SceneManager.LoadScene(1); 
        }

        if (Input.GetKeyDown(island2)) {
            Debug.Log("Pressed 2, to switch to island 2."); 
            SceneManager.LoadScene(2); 
        }
    }
}
