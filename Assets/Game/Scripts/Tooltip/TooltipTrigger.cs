using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float TooltipDelay = 0.5f;
    [SerializeField] InventoryItem inventoryItem;

    private WaitForSeconds tooltipDelayWFS;
    private Coroutine showTooltipCoroutine;

    void Awake()
    {
        tooltipDelayWFS = new WaitForSeconds(TooltipDelay);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        showTooltipCoroutine = StartCoroutine(ShowTooltipAfterDelay());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (showTooltipCoroutine != null)
        {
            StopCoroutine(showTooltipCoroutine);
        }
        TooltipSystem.Instance.Hide();
    }

    private IEnumerator ShowTooltipAfterDelay()
    {
        yield return tooltipDelayWFS;
        TooltipSystem.Instance.Show(inventoryItem.itemTemplate.Tag.ToString(),
            inventoryItem.itemTemplate.Description);
    }
}
