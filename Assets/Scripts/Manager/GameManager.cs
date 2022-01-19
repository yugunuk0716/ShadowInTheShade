using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    //�÷��̾�
    [SerializeField]
    public Transform player;
    public PlayerSO currentPlayerSO;

    //������Ʈ ���� Ÿ�ӽ�����
    [Range(0,1)]
    [SerializeField]
    private float _timeScale = 1f;

    //���� �ΰ��� �̺�Ʈ��
    public UnityEvent OnPlayerDash; //�÷��̾ �뽬�� �� ���� �̺�Ʈ
    public UnityEvent OnPlayerChangeType; //�÷��̾ �ڽ��� ���¸� �ٲ� �� ���� �̺�Ʈ

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
