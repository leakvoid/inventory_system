
using UnityEngine;
using UnityEngine.UI;

public class TooltipSystem : MonoBehaviour
{
    [SerializeField] private Tooltip tooltip;

    public static TooltipSystem Instance;
    public void Awake()
    {
        Instance = this;
    }

    public void Show(string header, string description)
    {
        tooltip.SetText(description, header);
        tooltip.UpdatePosition();
        tooltip.gameObject.SetActive(true);
    }

    public void Hide()
    {
        tooltip.gameObject.SetActive(false);
    }
}