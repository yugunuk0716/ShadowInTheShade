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
    [Header("�̵� �ӵ�"), Tooltip("�̵� �ӵ�")]
    public float SPD;

    [Header("�뽬 �ӵ�"), Tooltip("�뽬 �ӵ�")]
    public float DSP;

    [Header("�뽬 ���� �ð�"), Tooltip("�뽬 ���� �ð�")]
    public float DRT;

    [Header("�뽬 ��Ÿ��"), Tooltip("�뽬 ��Ÿ��")]
    public float DCT;

    [Header("ä�� ������ �߰� �̵��ӵ�"), Tooltip("ä�� ������ �߰� �̵��ӵ�")]
    public float HSP;
}


    [System.Serializable]
public struct PlayerAttackStats
{
    [Header("���ݷ�"), Tooltip("���ݷ�")]
    public float ATK;

    [Header("���ݼӵ�"), Tooltip("���ݼӵ�")]
    public float ASD;

    [Header("ũ��Ƽ�� Ȯ��"), Tooltip("ũ��Ƽ�� Ȯ��")]
    public float CTP;
    
    [Header("ũ��Ƽ�� ������"), Tooltip("ũ��Ƽ�� ������")]
    public float CTD;

    [Header("��ų ��Ÿ��"), Tooltip("��ų ��Ÿ��")]
    public float SCD;

    [Header("�� óġ�� �߰� ���ݷ�"), Tooltip("�� óġ�� �߰� ���ݷ�")]
    public float KAP;

    [Header("ȿ�� �߰� ���ݼӵ�"), Tooltip("ȿ�� �߰� ���ݼӵ�")]
    public float BSP;
}

[System.Serializable]
public struct PlayerECTStats
{
    [Header("�÷��̾� ����ġ"), Tooltip("�÷��̾� ����ġ")]
    public float EXP;

    [Header("�÷��̾� ü��"), Tooltip("�÷��̾� ü��")]
    public float PMH;

    [Header("�÷��̾� ����"), Tooltip("�÷��̾� ����")]
    public float LEV;

    [Header("Ÿ�ݽ� ȸ����"), Tooltip("Ÿ�ݽ� ȸ����")]
    public float APH;

    [Header("�÷��̾� ȸ�� Ȯ��"), Tooltip("�÷��̾� ȸ�� Ȯ��")]
    public float EVC;

    [Header("�뽬 �߰� ����"), Tooltip("�뽬 �߰� ����")]
    public float DPD;
}

[System.Serializable]
public struct PlayerMainStats
{
    [Header("��"), Tooltip("��")]
    public float STR;

    [Header("��ø"), Tooltip("��ø")]
    public float DEX;

    [Header("���"), Tooltip("���")]
    public float AGI;

    [Header("����"), Tooltip("����")]
    public float SPL;
}



[System.Serializable]
public struct PlayerPercentagePoint
{
    [Header("���ݷ� %"), Tooltip("���ݷ� %")]
    public float ATP;

    [Header("���� �߰� %"), Tooltip("���� �߰� %")]
    public float STP;

    [Header("���ݼӵ� %"), Tooltip("���ݼӵ� %")]
    public float SPP;
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

    [Header("�÷��̾��� %����")]
    public PlayerPercentagePoint PercentagePointStats;




    /*
        [Header("�÷��̾��� ������ ȿ�� Ȯ�� Bool")]
        public bool playerHasFranticherb = false;*/
}
