using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public HashSet<Item> Items = new HashSet<Item>();

    public void AddItem(Item item)
    {
        Items.Add(item);
    }
}

public class Item
{
    public string Id;
    public string UniqueId;
    public EquipmentType ItemType;

    public Item(EquipmentType itemType, string itemId)
    {
        Id = itemId;
        UniqueId = Guid.NewGuid().ToString();
        ItemType = itemType;
    }
}