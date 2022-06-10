using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerStates // �÷��̾� ����
{
    Human,
    Shadow
}

public enum PlayerJobState // �÷��̾� ����
{
    Default,
    Berserker,
    Archer,
    Greedy,
    Devilish
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

public enum PlayerDashState // �÷��̾� �뽬 ����
{
    Default,
    Power1,
    Power2,
    Power3,
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

    [Header("�뽬 ��Ÿ��")]
    public float DCT;

    [Header("ä�� ������ �߰� �̵��ӵ�")]
    public float HSP;
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

    [Header("�� óġ�� �߰� ���ݷ�")]
    public float KAP;
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

    [Header("Ÿ�ݽ� ȸ����")]
    public float APH;

    [Header("�÷��̾� ȸ�� Ȯ��")]
    public float EVC;

    [Header("�뽬 �߰� ����")]
    public float DPD;
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


    [Header("�÷��̾� ������ �ִϸ�����")]
    public List<Animator> playerClassAnimator;


    [Header("�÷��̾� ���� ����")]
    public bool canChangePlayerType;
    public PlayerStates playerStates;
    public PlayerJobState playerJobState;
    public PlayerDashState playerDashState;
    public PlayerInputState playerInputState;


    [Header("�÷��̾��� 4�� �⺻ ����")]
    public PlayerMainStats mainStats;


    [Header("�÷��̾��� �̵�����")]
    public PlayerMoveStats moveStats;

    
    [Header("�÷��̾��� ���ݽ���")]
    public PlayerAttackStats attackStats;

    [Header("�÷��̾��� ��Ÿ����")]
    public PlayerECTStats ectStats;


/*
    [Header("�÷��̾��� ������ ȿ�� Ȯ�� Bool")]
    public bool playerHasFranticherb = false;*/
}
