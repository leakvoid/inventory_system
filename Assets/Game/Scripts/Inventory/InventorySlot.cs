using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public const int MAX_ITEM_STACK = 99;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem draggedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        draggedItem.isHitAnySlot = true;

        if (transform.childCount == 0)
        {
            print("Drop item");
            // Empty slot
            if (draggedItem.parentAfterDrag.GetComponent<CraftingSlot>() != null)
            {
                VirtualInventoryContainer.Instance.Add(draggedItem);
            }

            draggedItem.parentAfterDrag = transform;
        }
        else
        {
            Transform itemInSlotTransform = transform.GetChild(0);
            InventoryItem itemInSlot = itemInSlotTransform.GetComponent<InventoryItem>();

            if (draggedItem.itemTemplate.Tag == itemInSlot.itemTemplate.Tag &&
                draggedItem.itemTemplate.Type == ItemType.Material)
            {
                print("Stack items");
                // Stack same materials
                if (draggedItem.ItemCount + itemInSlot.ItemCount <= MAX_ITEM_STACK)
                {
                    itemInSlot.ItemCount += draggedItem.ItemCount;
                    draggedItem.DestroyItem();
                }
                else
                {
                    draggedItem.ItemCount = itemInSlot.ItemCount + draggedItem.ItemCount - MAX_ITEM_STACK;
                    itemInSlot.ItemCount = MAX_ITEM_STACK;
                }
            }
            else
            {
                if (draggedItem.parentAfterDrag.GetComponent<CraftingSlot>() != null)
                    return;

                print("Swap items");
                // Swap parents
                itemInSlot.parentAfterDrag = draggedItem.parentAfterDrag;
                draggedItem.parentAfterDrag = transform;

                itemInSlotTransform.SetParent(itemInSlot.parentAfterDrag);
            }
        }
    }
}
