using UnityEngine;

public class ScrollViewContent : MonoBehaviour
{
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    void LateUpdate()
    {
        var p = rect.anchoredPosition;
        p.y = Mathf.Round(p.y);
        rect.anchoredPosition = p;
    }
}
