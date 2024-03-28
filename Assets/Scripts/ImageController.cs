using UnityEngine;

/// <summary>
/// 人の判定をするのかを可視化するためのImageのController
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class ImageController : MonoBehaviour
{
    private RectTransform _rectTransform;
    
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetImageToPeople(float x, float y, float width, float height)
    {
        var rectTransformLocalPosition = new Vector2();
        float newWidth = width * 2120f / 512f;
        float newHeight = height * 1325f / 512f;
        rectTransformLocalPosition.x = (x * 2120f / 512f) + (newWidth / 2f);
        rectTransformLocalPosition.y = ((y * 1325f / 512f) + (newHeight / 2f)) * -1f;
        _rectTransform.anchoredPosition = rectTransformLocalPosition;

        _rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }
}
