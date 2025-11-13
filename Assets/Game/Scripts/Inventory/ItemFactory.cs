using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] private InventoryItem inventoryItemPrefab;

    public static ItemFactory Instance;
    void Awake()
    {
        Instance = this;
    }

    public InventoryItem Create(Transform parent, ItemLocation location, ItemTemplate template, int count = 1)
    {
        InventoryItem item = Instantiate(inventoryItemPrefab, parent);
        item.Initialize(template, count);
        if (location == ItemLocation.Inventory)
        {
            VirtualInventoryContainer.Instance.Add(item);
        }
        return item;
    }
}