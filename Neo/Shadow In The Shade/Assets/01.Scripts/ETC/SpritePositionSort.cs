using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpritePositionSort : MonoBehaviour
{

    private Renderer rd;

    private void Awake()
    {
        rd = GetComponent<Renderer>();
        Sort();
    }

    private void LateUpdate()
    {
        Sort();
    }

    private void Sort()
    {
        float precisionMultiplier = 10.0f; // Sorting Order 정밀도 용
        rd.sortingOrder = (int)(-transform.position.y * precisionMultiplier);
    }

}