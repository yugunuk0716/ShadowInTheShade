using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    //플레이어
    [SerializeField]
    public Transform player;
    public PlayerSO currentPlayerSO;

    //오브젝트 관련 타임스케일
    [Range(0,1)]
    [SerializeField]
    private float _timeScale = 1f;

    //각종 인게임 이벤트들
    public UnityEvent OnPlayerDash; //플레이어가 대쉬할 때 쓰는 이벤트
    public UnityEvent OnPlayerChangeType; //플레이어가 자신의 상태를 바꿀 때 스는 이벤트

    //Cinemachine Camera
    public GameObject _cinemachineCamObj;

    public CinemachineConfiner _cinemachineCamConfiner;
    public CinemachineVirtualCamera _cinemachineCam;

    public void Awake()
    {
        Application.targetFrameRate = 300;
        _cinemachineCamConfiner = _cinemachineCamObj.GetComponent<CinemachineConfiner>();
        _cinemachineCam = _cinemachineCamObj.GetComponent<CinemachineVirtualCamera>();
        
    }

    private void Start()
    {
        init();
    }

    public void init()
    {
        _cinemachineCamConfiner.m_BoundingShape2D = PoolManager.Instance._rooms[0]._camBound;
        player.GetComponent<SpriteRenderer>().sprite = currentPlayerSO.playerSprite;
        currentPlayerSO.playerStates = PlayerStates.Human;
        currentPlayerSO.canChangePlayerType = true;
    }
}
