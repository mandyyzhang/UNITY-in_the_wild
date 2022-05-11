using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";

    // Inventory
    public Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;
    public bool obtainedItem = false;
    public bool treeShake = false; 
    
    public TMPro.TextMeshProUGUI interactionText;

    public Camera firstPersonCamera;

    private Vector3 treePos;
    public GameObject apple;

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
        Vector3 randomDir = Random.insideUnitCircle.normalized;
        Debug.Log(treePos);
        Vector3 spawnPos = new Vector3(treePos.x, treePos.y + 1.5f, treePos.z);
        GameObject appleSpawn = Instantiate(apple, spawnPos + randomDir, Quaternion.identity);
        appleSpawn.GetComponent<Rigidbody>().AddForce(randomDir *5f, ForceMode.Impulse);
    }

    private void HandleInteraction(Interactable interactable)
    {
        switch (interactable.interactionType) {
            case Interactable.InteractionType.Collect:
                obtainedItem = true;
                if (interactable.gameObject.GetComponent<WorldItem>().itemType == "glass shard")
                {
                    inventory.AddItem(new Item { itemType = Item.ItemType.GlassShards, amount = 1});
                } else if (interactable.gameObject.GetComponent<WorldItem>().itemType == "apple")
                {
                    inventory.AddItem(new Item { itemType = Item.ItemType.Apple, amount = 1});
                } 
                interactable.gameObject.SetActive(false);
                break;
                // helpful error for us in the future
            case Interactable.InteractionType.Harvest:
                // for future, drop the apple only, dont add to inventory directly
                treeShake = true;
                DropItem();
                //inventory.AddItem(new Item { itemType = Item.ItemType.Apple, amount = 1});
                break;
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
        

    }

}
