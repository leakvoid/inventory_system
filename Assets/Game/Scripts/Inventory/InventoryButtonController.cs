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

    void Awake()
    {
        showAllButton.interactable = false;

        inputField.onValueChanged.AddListener(OnInputValueChanged);
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
        InventoryManager.Instance.SortItems();
    }

    public void ShowAll()
    {
        inputField.text = "";
        showAllButton.interactable = false;
        toolsOnlyButton.interactable = true;
        materialsOnlyButton.interactable = true;

        InventoryManager.Instance.ShowAll();
    }

    public void ToolsOnly()
    {
        inputField.text = "";
        showAllButton.interactable = true;
        toolsOnlyButton.interactable = false;
        materialsOnlyButton.interactable = true;

        InventoryManager.Instance.ToolsOnlyFilter();
    }

    public void MaterialsOnly()
    {
        inputField.text = "";
        showAllButton.interactable = true;
        toolsOnlyButton.interactable = true;
        materialsOnlyButton.interactable = false;

        InventoryManager.Instance.MaterialsOnlyFilter();
    }
    
    public void OnInputValueChanged(string filter)
    {
        showAllButton.interactable = true;
        toolsOnlyButton.interactable = true;
        materialsOnlyButton.interactable = true;

        InventoryManager.Instance.PromptFilter(filter);
    }
}
