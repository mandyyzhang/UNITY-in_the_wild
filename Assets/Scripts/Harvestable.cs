using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : Interactable
{

    public override string GetDescription()
    {
        return "Shake";
    }

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }
}
