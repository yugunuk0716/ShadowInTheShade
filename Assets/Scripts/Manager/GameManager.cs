using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    //플레이어
    [SerializeField]
    private Transform player;
    public PlayerSO currentPlayerSO;

    //오브젝트 관련 타임스케일
    [Range(0,1)]
    [SerializeField]
    private float _timeScale = 1f;

    //각종 인게임 이벤트들
    public UnityEvent OnPlayerDash; //플레이어가 대쉬할 때 쓰는 이벤트
    public UnityEvent OnPlayerChangeType; //플레이어가 자신의 상태를 바꿀 때 스는 이벤트

    public void Awake()
    {
        init();
        Application.targetFrameRate = 300;
    }

    public void init()
    {
        player.GetComponent<SpriteRenderer>().sprite = currentPlayerSO.playerSprite;
        currentPlayerSO.playerStates = PlayerStates.Human;
        currentPlayerSO.canChangePlayerType = true;
    }
}
