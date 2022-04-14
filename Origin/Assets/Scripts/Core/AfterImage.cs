using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AfterImage : MonoBehaviour, IResettable
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite, Vector3 position)
    {
        transform.position = position;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        spriteRenderer.sprite = sprite;

        spriteRenderer.DOFade(0, 0.7f).OnComplete(() => {
            PoolManager.Instance._afterImagePool.Release(this); 
        });
    }

    public void Reset()
    {

    }
}
