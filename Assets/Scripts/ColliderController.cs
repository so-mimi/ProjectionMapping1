using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    [Header("大きくするほど左にいく"), SerializeField] private float offsetX = 0f;
    [Header("大きくするほど下に行く"), SerializeField] private float offsetY = 0f;
    [Header("大きくするほど幅が広くなる"), SerializeField] private float widthOffset = 1f;
    [Header("大きくするほど高さが広くなる"), SerializeField] private float heightOffset = 1f;
    
    public void ReshapeCollider(Vector2 centerPosition, float width, float height)
    {
        //コライダーを、四角形の右の辺に出す
        var RightTransform = this.transform;
        float newWidth = width * widthOffset;
        float newHeight = height * heightOffset;

        this.transform.position = new Vector3(centerPosition.x * 0.01415f + offsetX, centerPosition.y * 0.01369f + offsetY, 30f);
        this.transform.localScale = new Vector3(newWidth, newHeight, 70f);

    }
}
