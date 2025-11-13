using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualInventoryContainer : MonoBehaviour
{
    public List<InventoryItem> virtualInventory = new();

    public static VirtualInventoryContainer Instance;
    void Awake()
    {
        Instance = this;
    }

    public void Add(InventoryItem item)
    {
        if (virtualInventory.Count == InventoryManager.INVENTORY_SLOT_COUNT)
        {
            print("maximum capacity reached");
            return;
        }

        virtualInventory.Add(item);
    }

    public void Remove(InventoryItem item)
    {
        if (virtualInventory.Find(x => x == item) != null)
        {
            virtualInventory.Remove(item);
        }
    }
}

public enum ItemLocation
{
    Inventory,
    CraftingPanel
}