using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSort : MonoBehaviour
{

    private SpriteRenderer spriteRenderer = null;
    private float posOffsetY = 0.0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        posOffsetY = -transform.localPosition.y;
    }

    private void LateUpdate()
    {
        float precisionMultiplier = 5.0f; // Sorting Order 정밀도 용
        spriteRenderer.sortingOrder = (int)(-(transform.position.y + posOffsetY) * precisionMultiplier);


    }
}