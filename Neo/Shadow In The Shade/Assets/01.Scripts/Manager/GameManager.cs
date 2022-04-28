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

    public UnityEvent onPlayerDash; //플레이어가 대쉬할 때 쓰는 이벤트
    public UnityEvent<int> onPlayerAttack; //플레이어가 공격할 때 쓰는 이벤트
    public UnityEvent onPlayerTypeChanged; //플레이어가 자신의 상태를 바꾼 후 처리해야할 작업들을 사용할때 쓰는 이벤트
    public UnityEvent onPlayerChangingType; //플레이어가 자신의 상태를 바꾼 후 처리해야할 작업들을 사용할때 쓰는 이벤트
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
        onPlayerTypeChanged = new UnityEvent();
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
            PoolManager.Instance.CreatePool(so.poolPrefab, so.enemyName); //풀용 프리팹
        }



    }

}
