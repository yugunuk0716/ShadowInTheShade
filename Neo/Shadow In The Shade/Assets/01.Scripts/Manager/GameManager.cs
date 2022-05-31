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

    [Range(0f, 1f)]
    public float timeScale = 1f;

    public UnityEvent onPlayerDash; //플레이어가 대쉬할 때 쓰는 이벤트
    public UnityEvent<int> onPlayerAttack; //플레이어가 공격할 때 쓰는 이벤트
    public UnityEvent onPlayerTypeChanged; //플레이어가 자신의 상태를 바꾼 후 처리해야할 작업들을 사용할때 쓰는 이벤트
    public UnityEvent onPlayerChangeType; //플레이어가 자신의 상태를 바꾼 후 처리해야할 작업들을 사용할때 쓰는 이벤트
    public UnityEvent onPlayerHit;
    public UnityEvent onPlayerGetEXP;
    public UnityEvent<float, DiceType> onBossHpSend;
    public UnityEvent onPlayerGetItem;
    public UnityEvent onPlayerGetShadowOrb;

    public UnityEvent onEnemyHit;

    public UnityEvent<GameObject> onHumanDashCrossEnemy;


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
        onPlayerChangeType = new UnityEvent();
        onPlayerHit = new UnityEvent();
        onPlayerGetShadowOrb = new UnityEvent();


        player.GetComponentInChildren<SpriteRenderer>().sprite = playerSO.playerSprite;
        playerSO.playerStates = PlayerStates.Shadow;
        playerSO.playerDashState = PlayerDashState.Default;
        playerSO.moveStats.DSS = 1;
        playerSO.playerInputState = PlayerInputState.Idle;
        playerSO.canChangePlayerType = true;

        playerSO.ectStats.LEV = 0;
        playerSO.ectStats.EXP = 0;
        playerSO.ectStats.PMH = 400f;

        playerSO.mainStats.STR = 0f;
        playerSO.mainStats.DEX = 0f;
        playerSO.mainStats.AGI = 0f;
        playerSO.mainStats.SPL = 0f;

        playerSO.attackStats.ATK = 100f;
        playerSO.moveStats.SPD = 7f;
        playerSO.attackStats.ASD = 150f;
        playerSO.attackStats.CTP = 0f;
        playerSO.attackStats.CTD = 200f;


        foreach (PoolableMono p in poollingList.list)
        {
            PoolManager.Instance.CreatePool(p, null, p.count);
        }

        foreach (EnemyDataSO so in enemyList.enemyList)
        {
            PoolManager.Instance.CreatePool(so.poolPrefab, so.enemyName, so.poolPrefab.count); //풀용 프리팹
        }



    }

    public void InitMainStatPoint(int statsIndex)
    {
        switch(statsIndex)
        {
            case 1:
                float tempSTR = playerSO.mainStats.STR - 1;

                playerSO.attackStats.ATK -= tempSTR * 20f;

                playerSO.attackStats.ATK += playerSO.mainStats.STR * 20f;
                break;
            case 2:
                float tempDEX = playerSO.mainStats.DEX - 1;

                playerSO.moveStats.SPD -= tempDEX * .2f;
                playerSO.attackStats.ASD -= tempDEX * 6f;

                playerSO.moveStats.SPD += playerSO.mainStats.DEX * .2f;
                playerSO.attackStats.ASD += playerSO.mainStats.DEX * 6f;
                break;
            case 3:
                float tempAGI = playerSO.mainStats.AGI - 1;

                playerSO.attackStats.CTP -= tempAGI * 10;

                playerSO.attackStats.CTP += playerSO.mainStats.AGI * 10;


                break;
            case 4:
                float tempSPL = playerSO.mainStats.SPL - 1;

                playerSO.ectStats.PMH -= tempSPL * 50f;

                playerSO.ectStats.PMH += playerSO.mainStats.SPL * 50f;

                break;

        }
    }

}
