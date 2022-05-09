using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType {
        GlassShards,
        Apple,
    }

    public ItemType itemType; // count category only
    public int amount;

    public Sprite GetSprite() {
        switch (itemType) 
        {
            default:
            case ItemType.GlassShards:  
                return ItemAssets.Instance.glassSprite;
            case ItemType.Apple:  
                return ItemAssets.Instance.appleSprite;
        }
    }

    public bool isStackable()
    {
        switch (itemType) {
        default:
        case ItemType.GlassShards:
            return true;
        // case ItemType.Apple:
        //     return false;
        }
        
        
    }
}
