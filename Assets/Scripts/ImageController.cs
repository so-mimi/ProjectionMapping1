using UniRx;
using UnityEngine;

/// <summary>
/// 人の判定をするのかを可視化するためのImageのController
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class ImageController : MonoBehaviour
{
    private RectTransform _rectTransform;
    [SerializeField] private ColliderController _colliderController;
    [Header("大きくするほど右にいく"), SerializeField] private float offsetX = 0f;
    [Header("大きくするほど上に行く"), SerializeField] private float offsetY = 0f;
    [Header("大きくするほど幅が広くなる"), SerializeField] private float widthOffset = 1f;
    [Header("大きくするほど高さが広くなる"), SerializeField] private float heightOffset = 1f;

    
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetImageToPeople(float x, float y, float width, float height)
    {
        var rectTransformLocalPosition = new Vector2();
        float newWidth = width * 2120f / 512f * widthOffset;
        float newHeight = height * 1325f / 512f * heightOffset;
        rectTransformLocalPosition.x = (x * 2120f / 512f) + (newWidth / 2f) + offsetX ;
        rectTransformLocalPosition.y = ((y * 1325f / 512f) + (newHeight / 2f)) * -1f + offsetY;
        _rectTransform.anchoredPosition = rectTransformLocalPosition;
        _rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        
        //TODO: 衝突判定処理は依存関係を逆にしたいかも
        _colliderController.ReshapeCollider(rectTransformLocalPosition, width, height);
    }
}
