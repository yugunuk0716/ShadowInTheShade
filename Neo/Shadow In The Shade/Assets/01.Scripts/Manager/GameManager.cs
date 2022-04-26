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

        Init();
    }


    public Transform player;
    public bool isInvincible = false;
    public float defaultShadowGaugeSpeed = 1f;
    private float playerSpeed = 7f;

    [Range(0f, 1f)]
    public float timeScale = 1f;

    public UnityEvent onPlayerDash; //�÷��̾ �뽬�� �� ���� �̺�Ʈ
    public UnityEvent<int> onPlayerAttack; //�÷��̾ ������ �� ���� �̺�Ʈ
    public UnityEvent onPlayerChangeType; //�÷��̾ �ڽ��� ���¸� �ٲ� �� ó���ؾ��� �۾����� ����Ҷ� ���� �̺�Ʈ
    public UnityEvent onPlayerChangingType; //�÷��̾ �ڽ��� ���¸� �ٲ� �� ó���ؾ��� �۾����� ����Ҷ� ���� �̺�Ʈ
    public UnityEvent onPlayerHit;


    public FeedBackPlayer feedBackPlayer;

    public PlayerSO playerSO;


    [SerializeField] 
    private PoolingListSO poollingList;

    

    [SerializeField] 
    private EnemyListSO enemyList;


    public void Init()
    {
        onPlayerDash = new UnityEvent();
        onPlayerAttack = new UnityEvent<int>();
        onPlayerChangeType = new UnityEvent();
        onPlayerChangingType = new UnityEvent();
        onPlayerHit = new UnityEvent();


        player.GetComponentInChildren<SpriteRenderer>().sprite = playerSO.playerSprite;
        playerSO.playerStates = PlayerStates.Shadow;
        playerSO.moveStats.SPD = playerSpeed;
        playerSO.moveStats.DSS = 1;
        playerSO.playerInputState = PlayerInputState.Idle;
        playerSO.canChangePlayerType = true;

        foreach (PoolableMono p in poollingList.list)
        {
            PoolManager.Instance.CreatePool(p);
        }

        foreach (EnemyDataSO so in enemyList.enemyList)
        {
            PoolManager.Instance.CreatePool(so.poolPrefab, so.enemyName); //Ǯ�� ������
        }



    }

}