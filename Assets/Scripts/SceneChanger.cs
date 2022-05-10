using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneChanger : MonoBehaviour
{
    // Shortcut keys for changing islands 
    public KeyCode restart = KeyCode.R; 
    public KeyCode island1 = KeyCode.Alpha1; 
    public KeyCode island2 = KeyCode.Alpha2; 

    private Scene curr_scene; 

    // Note: Check build settings to see which index the scene is at. 

    // Referencing other scripts: 
    SelectionManager selectScript; 
    [SerializeField] GameObject selectionManager; 

    void Awake() {
        curr_scene = SceneManager.GetActiveScene(); 
        selectScript = selectionManager.GetComponent<SelectionManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // scene changes for debugging only
        if (Input.GetKeyDown(restart))
        {
            Debug.Log("Pressed R key to restart.");
            SceneManager.LoadScene(curr_scene.name);
        }
        if (Input.GetKeyDown(island1)) {
            Debug.Log("Pressed 1 to go to island 1."); 
            SceneManager.LoadScene(0); 
        }
        if (Input.GetKeyDown(island2)) {
            Debug.Log("Pressed 2 to go island 2."); 
            SceneManager.LoadScene(1);
        }

        #region Island 1 to Island 2 Transition 

        if (Input.GetKeyDown(KeyCode.T)) {
            int gemCount = selectScript.inventory.GetNumberOfGems(); 
            Debug.Log("Number of Gems in the Inventory = " + gemCount);
        }
        

        #endregion 

    }
}
