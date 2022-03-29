using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSort : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        float precisionMultiplier = 10.0f; // Sorting Order ���е� ��
        spriteRenderer.sortingOrder = (int)(-transform.position.y * precisionMultiplier);

    }
}