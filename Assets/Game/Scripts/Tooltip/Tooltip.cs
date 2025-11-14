using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headerField;
    [SerializeField] private TextMeshProUGUI contentField;
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] int characterWrapLimit;
    [SerializeField] private RectTransform rectTransform;

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;
        if (headerLength > characterWrapLimit || contentLength > characterWrapLimit)
        {
            layoutElement.enabled = true;
        }
        else
        {
            layoutElement.enabled = false;
        }
    }

    public void UpdatePosition()
    {
        Vector2 mousePos = Input.mousePosition;

        // float pivotX = position.x / Screen.width;
        // float pivotY = position.y / Screen.height;
        // rectTransform.pivot = new Vector2(pivotX, pivotY);
        float pivotX = mousePos.x > Screen.width * 0.8f ? 1 : 0;
        float pivotY = mousePos.y > Screen.height * 0.8f ? 1 : 0;
        rectTransform.pivot = new Vector2(pivotX, pivotY);
        
        transform.position = mousePos;
    }

    void Update()
    {
        UpdatePosition();
    }
}