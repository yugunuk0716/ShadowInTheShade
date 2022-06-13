using System;
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
    public UnityEvent onPlayerSkill; //플레이어가 스킬 쓸 때 쓰는 이벤트
    public UnityEvent onPlayerTypeChanged; //플레이어가 자신의 상태를 바꾼 후 처리해야할 작업들을 사용할때 쓰는 이벤트
    public UnityEvent onPlayerChangeType; //플레이어가 자신의 상태를 바꾼 후 처리해야할 작업들을 사용할때 쓰는 이벤트
    public UnityEvent<float> onPlayerHit;
    public UnityEvent onPlayerAttackSuccess;
    public UnityEvent onPlayerGetEXP;
    public UnityEvent<float, DiceType> onBossHpSend;
    public UnityEvent onPlayerGetItem;  
    public UnityEvent onPlayerGetSameItem;
    public UnityEvent onPlayerGetShadowOrb;
    public UnityEvent onPlayerStatUp;

    public UnityEvent onEnemyHit;

    public UnityEvent<GameObject> onHumanDashCrossEnemy;


    public FeedBackPlayer feedBackPlayer;

    public PlayerSO playerSO;


    [SerializeField] 
    private PoolingListSO poollingList;


    [SerializeField] 
    public EnemyListSO enemyList;
    


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (playerSO.playerJobState.Equals(PlayerJobState.Berserker))
            {
                playerSO.playerJobState = PlayerJobState.Default;
            }
            else if(playerSO.playerJobState.Equals(PlayerJobState.Default))
            {
                playerSO.playerJobState = PlayerJobState.Berserker;
            }
        }
    }

    public void Init()
    {
        onPlayerDash = new UnityEvent();
        onPlayerAttack = new UnityEvent<int>();
        onPlayerSkill = new UnityEvent();
        onPlayerTypeChanged = new UnityEvent();
        onPlayerChangeType = new UnityEvent();
        onPlayerHit = new UnityEvent<float>();
        onPlayerAttackSuccess = new UnityEvent();
        onPlayerGetShadowOrb = new UnityEvent();
        onPlayerGetSameItem = new UnityEvent();


        player.GetComponentInChildren<SpriteRenderer>().sprite = playerSO.playerSprite;
        playerSO.playerStates = PlayerStates.Shadow;
        playerSO.playerDashState = PlayerDashState.Default;
        playerSO.playerJobState = PlayerJobState.Default;
        playerSO.playerInputState = PlayerInputState.Idle;
        playerSO.canChangePlayerType = true;

        playerSO.ectStats.LEV = 0;
        playerSO.ectStats.EXP = 0;
        playerSO.ectStats.PMH = 400f;
        playerSO.ectStats.APH = 0;
        playerSO.ectStats.DPD = 0;
        playerSO.ectStats.EVC = 0;

        playerSO.mainStats.STR = 0f;
        playerSO.mainStats.DEX = 0f;
        playerSO.mainStats.AGI = 0f;
        playerSO.mainStats.SPL = 0f;

        playerSO.attackStats.ATK = 100f;
        playerSO.attackStats.ASD = 2f;
        playerSO.attackStats.CTP = 0f;
        playerSO.attackStats.CTD = 200f;
        playerSO.attackStats.KAP = 0f;
        playerSO.attackStats.SCD = 0f;

        playerSO.moveStats.SPD = 7f;
        playerSO.moveStats.HSP = 0f;

        foreach (PoolableMono p in poollingList.list)
        {
            if(p == null)
            {
                print("비엇눈데용");
                continue;
            }
            PoolManager.Instance.CreatePool(p, null, p.count);
        }

        foreach (EnemyDataSO so in enemyList.enemyList)
        {
            PoolManager.Instance.CreatePool(so.poolPrefab, so.enemyName, so.poolPrefab.count); //풀용 프리팹
        }



    }

    public void ApplyClassChange()
    {
        switch (playerSO.playerJobState)
        {
            case PlayerJobState.Default:
                break;
            case PlayerJobState.Berserker:
                break;
            case PlayerJobState.Archer:
                break;
            case PlayerJobState.Greedy:
                break;
            case PlayerJobState.Devilish:
                break;
        }

        //이거 하고 버튼 꺼줘야함
    }
    public void InitMainStatPoint(int statsIndex)
    {
        switch(statsIndex)
        {
            case 1:

                playerSO.mainStats.STR++;

                playerSO.attackStats.ATK += 20f + 20f * playerSO.PercentagePointStats.ATP / 100;

                playerSO.attackStats.ATK += 20f * playerSO.PercentagePointStats.ATP / 100;
                break;
            case 2:

                playerSO.mainStats.DEX++;

                playerSO.moveStats.SPD += .2f;
                playerSO.attackStats.ASD += 0.1f;


                playerSO.attackStats.ASD += 0.1f * playerSO.PercentagePointStats.SPP /100;

                playerSO.moveStats.SPD += .2f * playerSO.PercentagePointStats.ATP / 100;
                playerSO.attackStats.ASD += 0.1f * playerSO.PercentagePointStats.ATP / 100;
                break;
            case 3:

                playerSO.mainStats.AGI++;

                playerSO.attackStats.CTP += 10;


                playerSO.attackStats.CTP += 10 * playerSO.PercentagePointStats.ATP / 100;
                break;
            case 4:

                playerSO.mainStats.SPL++;

                playerSO.ectStats.PMH += 50f;


                playerSO.ectStats.PMH += 50f * playerSO.PercentagePointStats.ATP / 100;
                break;

        }
    }

}
