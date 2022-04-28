using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";

    // Inventory
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;
    public bool obtainedItem = false; 
    
    public TMPro.TextMeshProUGUI interactionText;

    public Camera firstPersonCamera;

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

                Interactable interactable = selection.GetComponent<Interactable>();

                if (interactable != null)
                {
                    Debug.Log("detect collectible");
                    //Debug.Log(interactable.GetDescription());
                    interactionText.text = interactable.GetDescription();

                    if (Input.GetMouseButtonDown(0))
                    {
                        HandleInteraction(interactable);
                    }
                    succesfulHit = true;
                }

                /*
                if (selection.CompareTag(selectableTag))
                {
                    var selectionRenderer = selection.GetComponent<Renderer>();

                    if (selectionRenderer != null)
                    {
                        interactionText.text = "Pick up";
                        if (Input.GetMouseButtonDown(0))
                        {
                            obtainedItem = true; 
                            selection.gameObject.SetActive(false);
                            // maybe set itemtype to variable
                            if (selection.gameObject.GetComponent<WorldItem>().itemType == "glass shard")
                            {
                                inventory.AddItem(new Item { itemType = Item.ItemType.GlassShards, amount = 1});
                            } else if (selection.gameObject.GetComponent<WorldItem>().itemType == "apple")
                            {
                                inventory.AddItem(new Item { itemType = Item.ItemType.Apple, amount = 1});
                            } 
                            
                        }
                        succesfulHit = true;
                    }
                }
                */
        
            }
            if (!succesfulHit) interactionText.text = "";
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
                } else if (interactable.gameObject.GetComponent<WorldItem>().itemType == "apple")
                {
                    inventory.AddItem(new Item { itemType = Item.ItemType.Apple, amount = 1});
                } 
                interactable.gameObject.SetActive(false);
                break;
                // helpful error for us in the future
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
        

    }

}
