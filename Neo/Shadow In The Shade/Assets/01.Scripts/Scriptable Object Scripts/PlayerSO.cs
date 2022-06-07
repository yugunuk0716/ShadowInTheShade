using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerStates // 플레이어 상태
{
    Human,
    Shadow
}

public enum PlayerInputState // 플레이어 인풋 상태
{
    Idle,
    Move,
    Dash,
    Change,
    Attack,
    Use,
    Hit
}

public enum PlayerDashState // 플레이어 대쉬 상태
{
    Default,
    Power1,
    Power2,
    Power3,
}

[System.Serializable]
public struct PlayerMoveStats
{
    [Header("이동 속도")]
    
    public float SPD;

    [Header("대쉬 속도")]
    public float DSP;

    [Header("대쉬 지속 시간")]
    public float DRT;

    [Header("대쉬 스택 추가 시간")]
    public float DST;

    [Header("대쉬 스택")]
    public int DSS;

    [Header("최대 대쉬 스택")]
    public int MDS;
}


[System.Serializable]
public struct PlayerAttackStats
{
    [Header("공격력")]
    public float ATK;

    [Header("공격속도")]
    public float ASD;

    [Header("크리티컬 확률")]
    public float CTP;
    
    [Header("크리티컬 데미지")]
    public float CTD;

    [Header("스킬 쿨타임")]
    public float SCD;
}

[System.Serializable]
public struct PlayerECTStats
{
    [Header("플레이어 경험치")]
    public float EXP;

    [Header("플레이어 체력")]
    public float PMH;

    [Header("플레이어 레벨")]
    public float LEV;

    [Header("타격시 회복량")]
    public float APH;

    [Header("타격시 회복량")]
    public float EVC;
}

[System.Serializable]
public struct PlayerMainStats
{
    [Header("힘")]
    public float STR;

    [Header("민첩")]
    public float DEX;

    [Header("욕망")]
    public float AGI;

    [Header("정신")]
    public float SPL;
}


[CreateAssetMenu(menuName = "SO/Player/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [Header("플레이어 스프라이트")]
    public Sprite playerSprite;

    [Header("플레이어 현제 상태")]
    public bool canChangePlayerType;
    public PlayerStates playerStates;
    public PlayerDashState playerDashState;
    public PlayerInputState playerInputState;


    [Header("플레이어의 4개 기본 스탯")]
    public PlayerMainStats mainStats;


    [Header("플레이어의 이동스탯")]
    public PlayerMoveStats moveStats;

    
    [Header("플레이어의 공격스탯")]
    public PlayerAttackStats attackStats;

    [Header("플레이어의 기타스탯")]
    public PlayerECTStats ectStats;
}
