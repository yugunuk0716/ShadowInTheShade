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
    public Vector2 _dir;

    //������Ʈ ���� Ÿ�ӽ�����
    [Range(0,1)]
    [SerializeField]
    public float _timeScale = 1f;

    //���� �ΰ��� �̺�Ʈ��
    public UnityEvent OnPlayerDash; //�÷��̾ �뽬�� �� ���� �̺�Ʈ
    public UnityEvent OnPlayerAttack; //�÷��̾ ������ �� ���� �̺�Ʈ
    public UnityEvent OnPlayerChangeType; //�÷��̾ �ڽ��� ���¸� �ٲ� �� ���� �̺�Ʈ

    public bool isAttack;

   

    public void Awake()
    {
        Application.targetFrameRate = 300;
    }

    private void Start()
    {
        init();
    }



    public void init()
    {
        player.GetComponent<SpriteRenderer>().sprite = currentPlayerSO.playerSprite;
        currentPlayerSO.playerStates = PlayerStates.Human;
        currentPlayerSO.canChangePlayerType = true;
    }
}
