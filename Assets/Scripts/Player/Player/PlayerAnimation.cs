using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer sr;
    private PlayerSO playerSO;
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerSO = GameManager.Instance.currentPlayerSO;
        GameManager.Instance.OnPlayerChangeType.AddListener(ChangePlayerTypeAnimation);
    }

    public void ChangePlayerTypeAnimation()
    {
        Color targetColor;

        if (playerSO.playerStates.Equals(PlayerStates.Human))
            targetColor = Color.white;
        else
            targetColor = Color.black;

        sr.DOColor(targetColor, playerSO.ectStats.TCT).OnComplete(() => 
        {
            playerSO.canChangePlayerType = true;
            GameManager.Instance.currentPlayerSO = playerSO;
        });
    }
}
