using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType {
        GlassShards,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() {
        switch (itemType) 
        {
            default:
            case ItemType.GlassShards:  
                return ItemAssets.Instance.glassSprite;
        }
    }

    public bool isStackable()
    {
        switch (itemType) {
        default:
        case ItemType.GlassShards:
            return true;
        }
    }
}
