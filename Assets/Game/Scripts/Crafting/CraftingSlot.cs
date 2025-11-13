using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem draggedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        draggedItem.isHitAnySlot = true;

        if (draggedItem.itemTemplate.Type != ItemType.Material ||
            transform.childCount > 0)
            return;

        if (draggedItem.ItemCount == 1)
        {
            draggedItem.parentAfterDrag = transform;
            draggedItem.transform.SetParent(transform);

            VirtualInventoryContainer.Instance.Remove(draggedItem);
        }
        else
        {
            ItemFactory.Instance.Create(transform, ItemLocation.CraftingPanel, draggedItem.itemTemplate);

            draggedItem.ItemCount--;
        }

        CraftingManager.Instance.UpdateCraftingOutcomeView();
    }
}
