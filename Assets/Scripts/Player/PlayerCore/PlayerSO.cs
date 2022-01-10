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
    [Header("이동속도")]
    public float SPD;

    [Header("대쉬쿨타임")]
    public float DCT;
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

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "PlayerSO",order = 0)]
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
}
