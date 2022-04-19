using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSort : MonoBehaviour
{
    public bool updateOnce = false;

    private SpriteRenderer spriteRenderer = null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (updateOnce)
            return;

        float precisionMultiplier = 10.0f; // Sorting Order 정밀도 용
        spriteRenderer.sortingOrder = (int)(-transform.position.y * precisionMultiplier);

    }
}