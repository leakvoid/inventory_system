using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonController : MonoBehaviour
{
    [SerializeField] Button showAllButton;
    [SerializeField] Button toolsOnlyButton;
    [SerializeField] Button materialsOnlyButton;
    [SerializeField] TMP_InputField inputField;

    enum CurrentMode
    {
        ShowAll,
        ToolsOnly,
        MaterialsOnly,
        Filter
    }

    CurrentMode currentMode;

    void Awake()
    {
        showAllButton.interactable = false;

        inputField.onValueChanged.AddListener(OnInputValueChanged);

        currentMode = CurrentMode.ShowAll;
    }

    public void PopulateSlots()
    {
        InventoryManager.Instance.PopulateEmptySlots();
    }

    public void DeleteAll()
    {
        var inventory = VirtualInventoryContainer.Instance.virtualInventory;
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            inventory[i].DestroyItem();
        }
    }

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

        switch (currentMode)
        {
            case CurrentMode.ToolsOnly:
                ToolsOnly();
                break;
            case CurrentMode.MaterialsOnly:
                MaterialsOnly();
                break;
            case CurrentMode.Filter:
                OnInputValueChanged(inputField.text);
                break;
            case CurrentMode.ShowAll:
            default:
                ShowAll();
                break;
        }
    }

    public void ShowAll()
    {
        inputField.text = "";
        showAllButton.interactable = false;
        toolsOnlyButton.interactable = true;
        materialsOnlyButton.interactable = true;
        currentMode = CurrentMode.ShowAll;

        InventoryManager.Instance.ShowAllView();
    }

    public void ToolsOnly()
    {
        inputField.text = "";
        showAllButton.interactable = true;
        toolsOnlyButton.interactable = false;
        materialsOnlyButton.interactable = true;
        currentMode = CurrentMode.ToolsOnly;

        InventoryManager.Instance.ToolsOnlyView();
    }

    public void MaterialsOnly()
    {
        inputField.text = "";
        showAllButton.interactable = true;
        toolsOnlyButton.interactable = true;
        materialsOnlyButton.interactable = false;
        currentMode = CurrentMode.MaterialsOnly;

        InventoryManager.Instance.MaterialsOnlyView();
    }
    
    public void OnInputValueChanged(string filter)
    {
        showAllButton.interactable = true;
        toolsOnlyButton.interactable = true;
        materialsOnlyButton.interactable = true;
        currentMode = CurrentMode.Filter;

        InventoryManager.Instance.PromptFilterView(filter);
    }
}
