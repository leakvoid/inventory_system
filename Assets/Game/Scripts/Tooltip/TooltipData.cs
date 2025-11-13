using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TooltipData", menuName = "UI/Tooltip Data")]
public class TooltipData : ScriptableObject
{
    [System.Serializable]
    public class LocalizedContent
    {
        public string languageCode;
        [TextArea(3, 10)]
        public string content;
    }

    public string tooltipID;
    public LocalizedContent[] localizedContents;
    public Sprite icon;
}