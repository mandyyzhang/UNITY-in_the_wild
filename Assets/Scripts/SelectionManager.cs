using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // Inventory
    public Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;
    public bool obtainedItem = false;
    public bool treeShake = false; 
    
    public TMPro.TextMeshProUGUI interactionText;

    public Camera firstPersonCamera;

    private Vector3 treePos;
    private Trees tree;
    public GameObject apple;

    // dialogue stuff 
    private DialogueManager dm; 
    [SerializeField] private GameObject dialogueManager; 

    private void Awake() {
        dm = dialogueManager.GetComponent<DialogueManager>(); 
    }

    private void Start()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }
    
    private void Update()
    {
        if (firstPersonCamera.enabled == true) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool succesfulHit = false;
        
            if (Physics.Raycast(ray, out hit, 3))
            {
                var selection = hit.transform;

                treePos = selection.transform.position;

                tree = selection.GetComponent<Trees>();

                Interactable interactable = selection.GetComponent<Interactable>();

                if (interactable != null)
                {
                    interactionText.text = interactable.GetDescription();

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (inventory.itemCount >= 5) {
                            Debug.Log("inventory full!");
                        } else {
                            HandleInteraction(interactable);
                        }
                    }
                    succesfulHit = true;
                }
        
            }
            if (!succesfulHit) interactionText.text = "";
        }
    }

    private void DropItem()
    {
        //Debug.Log(appleCount);
        if (tree.amount != 0)
        {
            Vector3 randomDir = Random.insideUnitCircle.normalized;
            Vector3 spawnPos = new Vector3(treePos.x, treePos.y + 1.5f, treePos.z);
            GameObject appleSpawn = Instantiate(apple, spawnPos + randomDir, Quaternion.identity);
            appleSpawn.GetComponent<Rigidbody>().AddForce(randomDir *5f, ForceMode.Impulse);
            tree.UpdateAppleCount();
            Debug.Log(tree.amount);
        } else {
            Debug.Log("no more apples!!");
        }
        

    }

    private void HandleInteraction(Interactable interactable)
    {
        switch (interactable.interactionType) {
            case Interactable.InteractionType.Collect:
                obtainedItem = true;
                if (interactable.gameObject.GetComponent<WorldItem>().itemType == "glass shard")
                {
                    inventory.AddItem(new Item { itemType = Item.ItemType.GlassShards, amount = 1});

                    // SHOW ADDED TO INVENTORY DIALOGUE
                    // FindObjectOfType<DialogueManager>().displayDialogue(0);
                    if (inventory.GetNumberOfGems() < 5) { 
                        dm.displayDialogue(0); // display dialogue at index 0 in sentences 
                    }
                    // when all 5 gems collected, show next island preview, then show dialogue for last gem (code in scene changer)

                } else if (interactable.gameObject.GetComponent<WorldItem>().itemType == "apple")
                {
                    inventory.AddItem(new Item { itemType = Item.ItemType.Apple, amount = 1});
                }
                Destroy(interactable.gameObject);
                //interactable.gameObject.SetActive(false);
                break;
                // helpful error for us in the future
            case Interactable.InteractionType.Harvest:
                treeShake = true;
                DropItem();
                //inventory.AddItem(new Item { itemType = Item.ItemType.Apple, amount = 1});
                break;
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
        

    }

}
