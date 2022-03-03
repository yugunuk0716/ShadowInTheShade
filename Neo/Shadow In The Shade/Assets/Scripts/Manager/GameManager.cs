using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("GameManager");
                obj.AddComponent<GameManager>();
                _instance = obj.GetComponent<GameManager>();
            }

            return _instance;
        }
    }

    public Transform _player;
    [Range(0f, 1f)]
    public float _timeScale = 1f;

    public UnityEvent _onPlayerDash; //플레이어가 대쉬할 때 쓰는 이벤트
    public UnityEvent _onPlayerAttack; //플레이어가 공격할 때 쓰는 이벤트
    public UnityEvent _onPlayerChangeType; //플레이어가 자신의 상태를 바꿀 때 스는 이벤트

    public PlayerSO _playerSO;


    private void Awake()
    {
        init();
    }


    public void init()
    {
        _player.GetComponent<SpriteRenderer>().sprite = _playerSO.playerSprite;
        _playerSO.playerStates = PlayerStates.Human;
        _playerSO.canChangePlayerType = true;
    }

}
