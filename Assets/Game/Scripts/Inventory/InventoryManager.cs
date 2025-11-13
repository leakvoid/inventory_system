using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySlot[] inventorySlots;

    [SerializeField] private ItemTemplate[] templates;

    public const int INVENTORY_SLOT_COUNT = 24;

    public static InventoryManager Instance;
    void Awake()
    {
        Instance = this;
    }

    public void ShowAllView()
    {
        ResetView();

        var inventoryItems = VirtualInventoryContainer.Instance.virtualInventory;
        for(int i = 0; i < inventoryItems.Count; i++)
        {
            inventoryItems[i].transform.SetParent(inventorySlots[i].transform);
            inventoryItems[i].gameObject.SetActive(true);
        }
    }

    public void ToolsOnlyView()
    {
        ResetView();

        var inventoryItems = VirtualInventoryContainer.Instance.virtualInventory;
        int i = 0;
        foreach(var item in inventoryItems)
        {
            if (item.itemTemplate.Type == ItemType.Tool)
            {
                item.transform.SetParent(inventorySlots[i].transform);
                item.gameObject.SetActive(true);
                i++;
            }
        }
    }

    public void MaterialsOnlyView()
    {
        ResetView();

        var inventoryItems = VirtualInventoryContainer.Instance.virtualInventory;
        int i = 0;
        foreach(var item in inventoryItems)
        {
            if (item.itemTemplate.Type == ItemType.Material)
            {
                item.transform.SetParent(inventorySlots[i].transform);
                item.gameObject.SetActive(true);
                i++;
            }
        }
    }

    public void PromptFilterView(string filter)
    {
        ResetView();

        var inventoryItems = VirtualInventoryContainer.Instance.virtualInventory;
        int i = 0;
        
        string searchFilter = filter.ToLower();
        foreach(var item in inventoryItems)
        {
            if (item.itemTemplate.Tag.ToString().ToLower().Contains(searchFilter))
            {
                item.transform.SetParent(inventorySlots[i].transform);
                item.gameObject.SetActive(true);
                i++;
            }
        }
    }
    
    void ResetView()
    {
        var inventory = VirtualInventoryContainer.Instance.virtualInventory;
        foreach(var item in inventory)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(transform.root);
        }
    }

    public void PopulateEmptySlots()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            if (slot.GetComponentInChildren<InventoryItem>() == null)
            {
                SpawnRandomItem(slot);
            }
        }
    }

    private void SpawnRandomItem(InventorySlot slot)
    {
        if (!HasEmptySlots())
            return;

        ItemTemplate template = templates[Random.Range(0, templates.Length)];
        if (template.Type == ItemType.Material)
        {
            ItemFactory.Instance.Create(slot.transform, ItemLocation.Inventory,
                template, Random.Range(InventorySlot.MAX_ITEM_STACK / 2, InventorySlot.MAX_ITEM_STACK));
        }
        else
        {
            ItemFactory.Instance.Create(slot.transform, ItemLocation.Inventory, template);
        }
    }

    public bool HasEmptySlots()
    {
        return VirtualInventoryContainer.Instance.virtualInventory.Count < INVENTORY_SLOT_COUNT;
    }

    public InventoryItem CreateNewItem(ItemTemplate template, int count)
    {
        if (HasEmptySlots())
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                if (slot.GetComponentInChildren<InventoryItem>() == null)
                {
                    InventoryItem inventoryItem = ItemFactory.Instance.Create(slot.transform, ItemLocation.Inventory, template, count);
                    return inventoryItem;
                }
            }
        }

        return null;
    }
}