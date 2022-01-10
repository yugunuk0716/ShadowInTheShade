using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer sr;
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        GameManager.Instance.OnPlayerChangeType.AddListener(ChangePlayerTypeAnimation);
    }

    public void ChangePlayerTypeAnimation()
    {
        Color targetColor;

        if (GameManager.Instance.currentPlayerSO.playerStates.Equals(PlayerStates.Human))
            targetColor = Color.white;
        else
            targetColor = Color.black;

        sr.DOColor(targetColor, .5f);
    }
}
