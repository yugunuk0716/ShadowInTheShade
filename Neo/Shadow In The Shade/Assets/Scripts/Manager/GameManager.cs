using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public Transform player;


    [Range(0f, 1f)]
    public float timeScale = 1f;

    public UnityEvent onPlayerDash; //플레이어가 대쉬할 때 쓰는 이벤트
    public UnityEvent onPlayerAttack; //플레이어가 공격할 때 쓰는 이벤트
    public UnityEvent onPlayerChangeType; //플레이어가 자신의 상태를 바꿀 때 스는 이벤트

    public PlayerSO playerSO;


    private void Start()
    {
        init();
    }


    public void init()
    {
        player.GetComponent<SpriteRenderer>().sprite = playerSO.playerSprite;
        playerSO.playerStates = PlayerStates.Human;
        playerSO.canChangePlayerType = true;
    }

}
