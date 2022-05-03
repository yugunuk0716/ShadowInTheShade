using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerStates // �÷��̾� ����
{
    Human,
    Shadow
}

public enum PlayerInputState // �÷��̾� ��ǲ ����
{
    Idle,
    Move,
    Dash,
    Change,
    Attack,
    Use,
    Hit
}

[System.Serializable]
public struct PlayerMoveStats
{
    [Header("�̵� �ӵ�")]
    
    public float SPD;

    [Header("�뽬 �ӵ�")]
    public float DSP;

    [Header("�뽬 ���� �ð�")]
    public float DRT;

    [Header("�뽬 ���� �߰� �ð�")]
    public float DST;

    [Header("�뽬 ����")]
    public int DSS;

    [Header("�ִ� �뽬 ����")]
    public int MDS;
}


[System.Serializable]
public struct PlayerAttackStats
{
    [Header("���ݷ�")]
    public float ATK;

    [Header("���ݼӵ�")]
    public float ASD;

    [Header("ũ��Ƽ�� Ȯ��")]
    public float CTP;
    
    [Header("ũ��Ƽ�� ������")]
    public float CTD;

    [Header("��ų ��Ÿ��")]
    public float SCD;
}

[System.Serializable]
public struct PlayerECTStats
{
    [Header("�÷��̾� ����ġ")]
    public float EXP;

    [Header("�÷��̾� ü��")]
    public float PMH;

    [Header("�÷��̾� ����")]
    public float LEV;
}

[System.Serializable]
public struct PlayerMainStats
{
    [Header("��")]
    public float STR;

    [Header("��ø")]
    public float DEX;

    [Header("���")]
    public float AGI;

    [Header("����")]
    public float SPL;
}


[CreateAssetMenu(menuName = "SO/Player/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [Header("�÷��̾� ��������Ʈ")]
    public Sprite playerSprite;

    [Header("�÷��̾� ���� ����")]
    public bool canChangePlayerType;
    public PlayerStates playerStates;
    public PlayerInputState playerInputState;


    [Header("�÷��̾��� 4�� �⺻ ����")]
    public PlayerMainStats mainStats;


    [Header("�÷��̾��� �̵�����")]
    public PlayerMoveStats moveStats;

    
    [Header("�÷��̾��� ���ݽ���")]
    public PlayerAttackStats attackStats;

    [Header("�÷��̾��� ��Ÿ����")]
    public PlayerECTStats ectStats;
}
