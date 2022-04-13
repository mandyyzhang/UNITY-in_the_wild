using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneChanger : MonoBehaviour
{
    public KeyCode restart = KeyCode.R; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(restart))
        {
            Debug.Log("Pressed R key to restart.");
            Scene curr_scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(curr_scene.name);
        }
    }
}
