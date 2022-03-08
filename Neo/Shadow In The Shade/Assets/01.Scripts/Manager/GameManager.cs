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

        init();
    }


    public Transform player;


    [Range(0f, 1f)]
    public float timeScale = 1f;

    public UnityEvent onPlayerDash; //�÷��̾ �뽬�� �� ���� �̺�Ʈ
    public UnityEvent<int> onPlayerAttack; //�÷��̾ ������ �� ���� �̺�Ʈ
    public UnityEvent onPlayerChangeType; //�÷��̾ �ڽ��� ���¸� �ٲ� �� ���� �̺�Ʈ

    public UnityEvent onStateEnter;
    public UnityEvent onStateEnd;

    public PlayerSO playerSO;


    [SerializeField] private PoolingListSO _poollingList;
    

    [SerializeField] private EnemyListSO _enemyList;


    public void init()
    {
        player.GetComponentInChildren<SpriteRenderer>().sprite = playerSO.playerSprite;
        playerSO.playerStates = PlayerStates.Human;
        playerSO.playerInputState = PlayerInputState.Idle;
        playerSO.canChangePlayerType = true;

        foreach (PoolableMono p in _poollingList.list)
        {
            PoolManager.Instance.CreatePool(p);
        }

        foreach (EnemyDataSO so in _enemyList.enemyList)
        {
            PoolManager.Instance.CreatePool(so.poolPrefab, so.type.ToString()); //Ǯ�� ������
        }
    }

}
