using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum ItemRarity
{
    Normal,
    Rare,
    Legendary
}


[CreateAssetMenu(menuName = "SO/Item/ItemDataSO")]
public class Item : ScriptableObject
{
    [Header("아이템 기본 정보")]
    public string itemName;
    public Sprite itemSprite;
    public ItemRarity rarity;

    [Header("아이템 설명")]
    public string itemAbility;
    public string itemComment;

    [Header("아이템 스탯")]
    public float attackPoint;
    public float maxHpPoint;
    public float attackSpeedPoint;
    public float moveSpeedPoint;
    public float criticalPercentagePoint;
    public float criticalPowerPoint;
    public float shadowGaugePoint;
/*
    Item()
    {
        attackPoint = 0f;
        maxHpPoint = 0f;
        attackSpeedPoint = 0f;
        moveSpeedPoint = 0f;
        criticalPercentagePoint = 0f;
        criticalPowerPoint = 0f;
        shadowGaugePoint = 0f;
    }

    public void SetItemStats(float _attackPoint = 0f, float _maxHpPoint = 0f, float _attackSpeedPoint = 0f, float _moveSpeedPoint = 0f,
        float _criticalPercentagePoint = 0f, float _criticalPowerPoint = 0f, float _shadowGaugePoint = 0f)
    {
        this.attackPoint = _attackPoint;
        this.maxHpPoint = _maxHpPoint;
        this.attackSpeedPoint = _attackSpeedPoint;
        this.moveSpeedPoint = _moveSpeedPoint;
        this.criticalPercentagePoint = _criticalPercentagePoint;
        this.criticalPowerPoint = _criticalPowerPoint;
        this.shadowGaugePoint = _shadowGaugePoint;
    }*/
}
