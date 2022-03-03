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

    public UnityEvent _onPlayerDash; //�÷��̾ �뽬�� �� ���� �̺�Ʈ
    public UnityEvent _onPlayerAttack; //�÷��̾ ������ �� ���� �̺�Ʈ
    public UnityEvent _onPlayerChangeType; //�÷��̾ �ڽ��� ���¸� �ٲ� �� ���� �̺�Ʈ

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
