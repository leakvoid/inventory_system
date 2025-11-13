using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler,
    IDragHandler, IEndDragHandler, IPointerClickHandler
{
    // Data
    [HideInInspector] public ItemTemplate itemTemplate;
    private int itemCount = 1;
    public int ItemCount
    {
        get => itemCount;
        set
        {
            itemCount = value;
            countText.text = itemCount.ToString();
            if (itemCount > 1)
            {
                countText.gameObject.SetActive(true);
            }
            else
            {
                countText.gameObject.SetActive(false);
            }
        }
    }

    // UI
    public Image image;
    public TextMeshProUGUI countText;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public bool isHitAnySlot;

    public void Initialize(ItemTemplate template, int count = 1)
    {
        itemTemplate = template;
        ItemCount = count;

        if (itemTemplate.Type == ItemType.Tool || ItemCount == 1)
        {
            countText.gameObject.SetActive(false);
        }
        image.sprite = itemTemplate.Image;
    }

    // Split items
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            return;

        if (itemTemplate.Type != ItemType.Material || ItemCount <= 1)
            return;

        SplitStack();
    }
    
    private void SplitStack()
    {
        int firstHalf = ItemCount / 2;
        int secondHalf = ItemCount - firstHalf;

        ItemCount = firstHalf;

        InventoryItem newItem = InventoryManager.Instance.CreateNewItem(itemTemplate, secondHalf);
        if (newItem == null)
        {
            ItemCount = secondHalf + firstHalf;
            return;
        }
    }

    // Drag and drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("Begin item drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isHitAnySlot)
        {
            print("End item drag");
            isHitAnySlot = false;
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }
        else
        {
            DestroyItem();
        }

    }

    public void DestroyItem()
    {
        VirtualInventoryContainer.Instance.Remove(this);
        Destroy(gameObject);
    }
}
