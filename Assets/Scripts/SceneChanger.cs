using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
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

    // Referencing other scripts: ITS A MESS T.T
    private SelectionManager selectScript; 
    private IslandGeneration island;
    private FirstPersonController fpc; 
     
    [SerializeField] private GameObject canvasPopUp; 
    [SerializeField] private GameObject selectionManager; 
    [SerializeField] private GameObject islandGeneration;
    [SerializeField] private GameObject firstPersonController; 

    private CollisionTrigger ctTop; 
    private CollisionTrigger ctBottom; 
    private CollisionTrigger ctLeft; 
    private CollisionTrigger ctRight;  

    [SerializeField] private GameObject collisionTriggerTop; 
    [SerializeField] private GameObject collisionTriggerBottom; 
    [SerializeField] private GameObject collisionTriggerLeft; 
    [SerializeField] private GameObject collisionTriggerRight; 

    //private variables 
    private int gemCount; 

    private Canvas nextIslandPic; 
    private CanvasGroup nextIslandPreview;
    private bool displayedNextIsland = false; 

    private bool CanvasFadeIn = false; 
    private bool CanvasFadeOut = false; 
    private string nextSceneName; 

    public bool collectedAllGems = false; 
    

    void Awake() {
        curr_scene = SceneManager.GetActiveScene(); 
        fpc = firstPersonController.GetComponent<FirstPersonController>(); 
        selectScript = selectionManager.GetComponent<SelectionManager>(); 
        island = islandGeneration.GetComponent<IslandGeneration>();  
        nextIslandPic = canvasPopUp.GetComponent<Canvas>();
        nextIslandPreview = canvasPopUp.GetComponent<CanvasGroup>(); 
        nextIslandPreview.alpha = 0; 
        nextIslandPic.enabled = false; 

        ctTop = collisionTriggerTop.GetComponent<CollisionTrigger>(); 
        ctBottom = collisionTriggerBottom.GetComponent<CollisionTrigger>(); 
        ctLeft = collisionTriggerLeft.GetComponent<CollisionTrigger>(); 
        ctRight = collisionTriggerRight.GetComponent<CollisionTrigger>(); 
    }

    void Start() {
        collectedAllGems = false; 
        if (curr_scene.name == "Island 1") {
            nextSceneName = "Island 2"; 
        }
        else if (curr_scene.name == "Island 2") {
            nextSceneName = "Island 3"; 
        }
        else if (curr_scene.name == "Island 3") {
            nextSceneName = "Island 4"; 
        }
        else if (curr_scene.name == "Island 4") {
            // move to end of game scene (which we dont have so move back to island 1 for now)
            nextSceneName = "Island 1"; 
        }
        else {
            Debug.Log("nextSceneName not determined. Assign nextSceneName in SceneChanger");
        }
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
        // FOR DEBUGGING ONLY: Press G to trigger collectedAllGems *Remember to comment out*
        if (Input.GetKeyDown(KeyCode.G)) {
            collectedAllGems = !collectedAllGems; 
            Debug.Log("Currently collectedAllGems = " + collectedAllGems); 
        }
        // for debugging only 
        if (Input.GetKeyDown(KeyCode.T)) {
            int gemCount = selectScript.inventory.GetNumberOfGems(); 
            Debug.Log("Number of Gems in the Inventory = " + gemCount);
        }
        // for debugging only ^ 

        #region Island to Island Transition (Triggered when all (5) gems collected)

        #region Show Island 2 Preview & check if collected all gems
        
        // only calculate gemCount when we obtained an item
        if (selectScript.obtainedItem) {
            gemCount = selectScript.inventory.GetNumberOfGems(); 
        }
        
        if (gemCount == island.gemsToSpawn) {
            
            collectedAllGems = true; 

            if (!displayedNextIsland) {
                nextIslandPic.enabled = true;
                CanvasFadeIn = true; 
                fpc.pauseControls = true;
                displayedNextIsland = true;
                Debug.Log("Collected all " + gemCount + " gems");
            }
            
        }
        
        if (nextIslandPic.enabled && Input.GetMouseButtonDown(0)) {
            CanvasFadeOut = true; 
        }

        if (CanvasFadeIn) {
            if (nextIslandPreview.alpha >= 1) {
                CanvasFadeIn = false; 
            }
            nextIslandPreview.alpha += Time.deltaTime;  
        }

        if (CanvasFadeOut) {
            if (nextIslandPreview.alpha <= 0) {
                CanvasFadeOut = false; 
                nextIslandPic.enabled = false;  
                fpc.pauseControls = false;
            }
            nextIslandPreview.alpha -= Time.deltaTime; 
        }
        #endregion // end of Show Island 2 Preview region 

        #region End of World Collision Trigger (when you collect all the gems on the current island, moves you to next island)

        if (collectedAllGems && (ctTop.collidedTop || ctBottom.collidedBottom || 
            ctLeft.collidedLeft || ctRight.collidedRight)) 
        {
            Debug.Log("Moving on to " + nextSceneName);
            SceneManager.LoadScene(nextSceneName);
        }

        #endregion // end of End of World Collision Trigger region 

        #endregion // end of Island to Island transition region 

    }

}
