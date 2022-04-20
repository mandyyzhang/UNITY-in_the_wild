using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    // [SerializeField] private Material highlightMaterial;
    // [SerializeField] private Material defaultMaterial;


    public TMPro.TextMeshProUGUI interactionText;
    
    private void Update()
    {
        
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
                        Debug.Log("diamond clicked");
                        selection.gameObject.SetActive(false);
                    }
                    succesfulHit = true;
                }
            }
        }
        if (!succesfulHit) interactionText.text = "";

    }
}
