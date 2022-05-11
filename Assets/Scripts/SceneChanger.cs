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
    public KeyCode island3 = KeyCode.Alpha3; 
    public KeyCode island4 = KeyCode.Alpha4; 

    private Scene curr_scene; 

    // Note: Check build settings to see which index the scene is at. 

    // Referencing other scripts: 
    private SelectionManager selectScript; 
    private IslandGeneration island; 
    [SerializeField] private GameObject canvasPopUp; 
    [SerializeField] private GameObject selectionManager; 
    [SerializeField] private GameObject islandGeneration; 

    //private variables 
    private int gemCount; 

    private Canvas nextIslandPic; 
    private bool displayedNextIsland = false; 

    void Awake() {
        curr_scene = SceneManager.GetActiveScene(); 
        selectScript = selectionManager.GetComponent<SelectionManager>(); 
        island = islandGeneration.GetComponent<IslandGeneration>(); 
        nextIslandPic = canvasPopUp.GetComponent<Canvas>();
        nextIslandPic.enabled = false; 
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
            SceneManager.LoadScene(1); 
        }
        if (Input.GetKeyDown(island2)) {
            Debug.Log("Pressed 2 to go island 2."); 
            SceneManager.LoadScene(2);
        }
        if (Input.GetKeyDown(island3)) {
            Debug.Log("Pressed 3 to go island 3."); 
            SceneManager.LoadScene(3);
        }
        if (Input.GetKeyDown(island4)) {
            Debug.Log("Pressed 4 to go island 4."); 
            SceneManager.LoadScene(4);
        }

        #region Island 1 to Island 2 Transition 

        // for debugging only 
        if (Input.GetKeyDown(KeyCode.T)) {
            int gemCount = selectScript.inventory.GetNumberOfGems(); 
            Debug.Log("Number of Gems in the Inventory = " + gemCount);
        }
        // for debugging only ^ 
        if (selectScript.obtainedItem) {
            gemCount = selectScript.inventory.GetNumberOfGems(); 
        }
        
        if (gemCount == island.gemsToSpawn && !displayedNextIsland) {
            nextIslandPic.enabled = true; 
            displayedNextIsland = true;
            Debug.Log("Collected all " + gemCount + " gems");
        }
        
        if (nextIslandPic.enabled && Input.GetMouseButtonDown(0)) {
            nextIslandPic.enabled = false; 
        }

        #endregion 

    }
}
