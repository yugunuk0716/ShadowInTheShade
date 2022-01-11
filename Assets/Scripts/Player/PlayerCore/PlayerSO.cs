using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerStates // �÷��̾� ����
{
    Human,
    Shadow
}

[System.Serializable]
public struct PlayerMoveStats
{
    [Header("�̵��ӵ�")]
    public float SPD;

    [Header("�뽬��Ÿ��")]
    public float DCT;
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

    [Header("��ų ��Ÿ��")]
    public float SCD;
}

[System.Serializable]
public struct PlayerECTStats
{
    [Header("Ÿ�� ���� �ð�")]
    public float TCT;
}


[CreateAssetMenu(fileName = "ScriptableObject", menuName = "PlayerSO",order = 0)]
public class PlayerSO : ScriptableObject
{
    [Header("�÷��̾� ��������Ʈ")]
    public Sprite playerSprite;

    [Header("�÷��̾� ���� ����")]
    public bool canChangePlayerType;
    public PlayerStates playerStates;

    [Header("�÷��̾��� �̵�����")]
    public PlayerMoveStats moveStats;

    
    [Header("�÷��̾��� ���ݽ���")]
    public PlayerAttackStats attackStats;

    [Header("�÷��̾��� ��Ÿ����")]
    public PlayerECTStats ectStats;
}
