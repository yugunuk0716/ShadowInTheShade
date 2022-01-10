using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    //�÷��̾�
    [SerializeField]
    private Transform player;
    public PlayerSO currentPlayerSO;

    //������Ʈ ���� Ÿ�ӽ�����
    [Range(0,1)]
    [SerializeField]
    private float _timeScale = 1f;

    //���� �ΰ��� �̺�Ʈ��
    public UnityEvent OnPlayerDash; //�÷��̾ �뽬�� �� ���� �̺�Ʈ
    public UnityEvent OnPlayerChangeType; //�÷��̾ �ڽ��� ���¸� �ٲ� �� ���� �̺�Ʈ


}
