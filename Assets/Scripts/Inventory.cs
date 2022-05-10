using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;

    public int itemCount = 0;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
       if (itemCount < 5)
       {
           if (item.isStackable())
           {
               bool itemAlreadyInInventory = false;
               foreach (Item inventoryItem in itemList)
               {
                   if (inventoryItem.itemType == item.itemType)
                   {
                       inventoryItem.amount += item.amount;
                       itemAlreadyInInventory = true;
                   }
               }
               if (!itemAlreadyInInventory)
               {
                   itemList.Add(item);
                   itemCount++;
               }
           } else {
               itemList.Add(item);
               itemCount++;
           }
           Debug.Log(itemCount);
           OnItemListChanged?.Invoke(this, EventArgs.Empty);
       }
    }

    public void RemoveItem(Item item)
    {
        if (item.isStackable())
       {
           Item itemInInventory = null;
           foreach (Item inventoryItem in itemList)
           {
               if (inventoryItem.itemType == item.itemType)
               {
                   inventoryItem.amount -= item.amount;
                   itemInInventory = inventoryItem;
               }
           }
           if (itemInInventory != null && itemInInventory.amount <= 0) {
               itemList.Remove(itemInInventory);
               itemCount--;
           }
       } else {
           itemList.Remove(item);
           itemCount--;
       }
       OnItemListChanged?.Invoke(this, EventArgs.Empty);

    }

    public List<Item> GetItemList()
    {
       return itemList;
    }

    public int GetNumberOfGems()
    { 
        foreach (Item inventoryItem in itemList) {
            if (inventoryItem.itemType == Item.ItemType.GlassShards) {
                return inventoryItem.amount;
            }
        }

        Debug.Log("Unable to find the number of shards in the inventory.");
        return -1; 
    }
    
}
