using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float TooltipDelay = 0.4f;
    WaitForSeconds tooltipDelayWFS;

    public string Content;
    public string Header;
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
        TooltipSystem.Hide();
    }

    private IEnumerator ShowTooltipAfterDelay()
    {
        yield return tooltipDelayWFS;
        TooltipSystem.Show(Content, Header);
    }
}
