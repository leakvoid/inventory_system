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

    void Start()
    {
        PopulateEmptySlots();
        currentMode = CurrentMode.ShowAll;
    }

    enum CurrentMode
    {
        ShowAll,
        ToolsOnly,
        MaterialsOnly,
        Filter
    }
    CurrentMode currentMode;

    // Sorting
    public void SortItems()
    {
        VirtualInventoryContainer.Instance.virtualInventory.Sort((a, b) => {
            int tagComparison = string.Compare(
                a.itemTemplate.Tag.ToString(),
                b.itemTemplate.Tag.ToString(),
                true);

            if (tagComparison != 0)
                return tagComparison;

            return b.ItemCount.CompareTo(a.ItemCount);
        });

        UpdateView();
    }

    // Filtering
    public void UpdateView()
    {
        switch (currentMode)
        {
            case CurrentMode.ToolsOnly:
                ToolsOnlyFilter();
                break;
            case CurrentMode.MaterialsOnly:
                MaterialsOnlyFilter();
                break;
            case CurrentMode.Filter:
                PromptFilter(lastPrompt);
                break;
            case CurrentMode.ShowAll:
            default:
                ShowAll();
                break;
        }
    }

    public void ShowAll()
    {
        ResetView();
        currentMode = CurrentMode.ShowAll;

        var inventoryItems = VirtualInventoryContainer.Instance.virtualInventory;
        for(int i = 0; i < inventoryItems.Count; i++)
        {
            inventoryItems[i].transform.SetParent(inventorySlots[i].transform);
            inventoryItems[i].gameObject.SetActive(true);
        }
    }

    public void ToolsOnlyFilter()
    {
        ResetView();
        currentMode = CurrentMode.ToolsOnly;

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

    public void MaterialsOnlyFilter()
    {
        ResetView();
        currentMode = CurrentMode.MaterialsOnly;

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

    string lastPrompt;
    public void PromptFilter(string filter)
    {
        ResetView();
        currentMode = CurrentMode.Filter;

        var inventoryItems = VirtualInventoryContainer.Instance.virtualInventory;
        int i = 0;
        
        lastPrompt = filter.ToLower();
        foreach(var item in inventoryItems)
        {
            if (item.itemTemplate.Tag.ToString().ToLower().Contains(lastPrompt))
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

    // Item creation
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

        UpdateView();
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