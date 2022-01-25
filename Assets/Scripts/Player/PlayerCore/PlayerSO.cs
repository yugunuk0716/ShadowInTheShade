using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerStates // 플레이어 상태
{
    Human,
    Shadow
}

[System.Serializable]
public struct PlayerMoveStats
{
    [Header("이동 속도")]
    public float SPD;

    [Header("대쉬 속도")]
    public float DPD;

    [Header("대쉬 쿨타임")]
    public float DCT;

    [Header("대쉬 시간")]
    public float DRT;

    [Header("대쉬 스택")]
    public int DSS;
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

    [Header("스킬 쿨타임")]
    public float SCD;
}

[System.Serializable]
public struct PlayerECTStats
{
    [Header("타입 변경 시간")]
    public float TCT;

    [Header("마우스를 보는 시간")]
    public float LPS;
}


[CreateAssetMenu(menuName = "SO/Player/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [Header("플레이어 스프라이트")]
    public Sprite playerSprite;

    [Header("플레이어 현제 상태")]
    public bool canChangePlayerType;
    public PlayerStates playerStates;

    [Header("플레이어의 이동스탯")]
    public PlayerMoveStats moveStats;

    
    [Header("플레이어의 공격스탯")]
    public PlayerAttackStats attackStats;

    [Header("플레이어의 기타스탯")]
    public PlayerECTStats ectStats;
}
