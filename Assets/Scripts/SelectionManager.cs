using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    // [SerializeField] private Material highlightMaterial;
    // [SerializeField] private Material defaultMaterial;

    // Inventory
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    public AudioSource collectGlassSound;
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
        
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag(selectableTag))
                {
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    if (selectionRenderer != null)
                    {
                        interactionText.text = "Pick up";
                        if (Input.GetMouseButtonDown(0))
                        {
                            collectGlassSound.Play();
                                Debug.Log("diamond clicked");
                            selection.gameObject.SetActive(false);
                            // hardcoded for now
                            inventory.AddItem(new Item { itemType = Item.ItemType.GlassShards, amount = 1});
                        }
                        succesfulHit = true;
                    }
                }
            }
            if (!succesfulHit) interactionText.text = "";
        }
    }

}
