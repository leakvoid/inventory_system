using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] private CraftingSlot[] craftingSlots;
    [SerializeField] private CraftingRecipe[] recipes;
    [SerializeField] private Transform outcomeView;
    [SerializeField] private Button craftItemButton;

    private Dictionary<ItemTag, InventoryItem> dummyCrafts;

    public static CraftingManager Instance;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        dummyCrafts = new Dictionary<ItemTag, InventoryItem>();

        foreach (var recipe in recipes)
        {
            InventoryItem inventoryItem = ItemFactory.Instance.Create(outcomeView, ItemLocation.CraftingPanel,
                recipe.CraftingOutcome, recipe.CraftedAmount);
 
            inventoryItem.gameObject.SetActive(false);
            inventoryItem.transform.SetParent(transform.root);

            dummyCrafts[recipe.CraftingOutcome.Tag] = inventoryItem;
        }

    }

    public void UpdateCraftingOutcomeView()
    {
        // void crafting output
        InventoryItem craftPreviewItem = outcomeView.GetComponentInChildren<InventoryItem>();
        if (craftPreviewItem)
        {
            craftPreviewItem.gameObject.SetActive(false);
            craftPreviewItem.transform.SetParent(transform.root);
        }
        craftItemButton.interactable = false;

        // check
        int woodCount = 0;
        int ironCount = 0;
        int stickCount = 0;
        int stoneCount = 0;

        foreach (var slot in craftingSlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot)
            {
                switch (itemInSlot.itemTemplate.Tag)
                {
                    case ItemTag.Wood:
                        woodCount++;
                        break;
                    case ItemTag.Iron:
                        ironCount++;
                        break;
                    case ItemTag.Stick:
                        stickCount++;
                        break;
                    case ItemTag.Stone:
                        stoneCount++;
                        break;
                    default:
                        throw new System.Exception("Unknown crafting material");
                }
            }
        }

        foreach (var recipe in recipes)
        {
            if (recipe.WoodRequired == woodCount && recipe.StickRequired == stickCount &&
                recipe.IronRequired == ironCount && recipe.StoneRequired == stoneCount)
            {
                InventoryItem dummy = dummyCrafts[recipe.CraftingOutcome.Tag];
                dummy.transform.SetParent(outcomeView);
                dummy.gameObject.SetActive(true);
                craftItemButton.interactable = true;
                return;
            }
        }
    }
    
    public void CraftItem()
    {
        if (!InventoryManager.Instance.HasEmptySlots())
            return;

        InventoryItem dummyCraft = outcomeView.GetComponentInChildren<InventoryItem>();
        if (!dummyCraft)
        {
            throw new System.Exception("Crafting error");
        }

        InventoryManager.Instance.CreateNewItem(dummyCraft.itemTemplate, dummyCraft.ItemCount);

        // cleanup
        dummyCraft.gameObject.SetActive(false);
        dummyCraft.transform.SetParent(transform.root);
        craftItemButton.interactable = false;

        foreach (var slot in craftingSlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot)
            {
                itemInSlot.DestroyItem();
            }
        }
    }
}
