using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject shadowWall;
    public GameObject normalWall;

    private void Start()
    {
        GameManager.Instance.onPlayerTypeChanged.AddListener(() =>
        {
            shadowWall.SetActive(PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
            normalWall.SetActive(!PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
        });
    }
}
