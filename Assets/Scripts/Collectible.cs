using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Interactable
{

    public override string GetDescription()
    {
        return "Pick up";
    }

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }
}
