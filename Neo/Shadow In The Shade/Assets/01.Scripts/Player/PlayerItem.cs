using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public Item getItme;

    public List<Item> playerHasItems = new List<Item>();

    private void Update()
    {
        if(getItme != null)
        {
            AddingItem(getItme);
        }
    }

    public void AddingItem(Item item)
    {
        ItemSO iSo = item.itemData;
        PlayerSO pSo = GameManager.Instance.playerSO;

        pSo.attackStats.ATK += iSo.attackPoint;
        pSo.ectStats.PMH += iSo.maxHpPoint;
        pSo.attackStats.ASD += iSo.attackSpeedPoint;
        pSo.moveStats.SPD += iSo.moveSpeedPoint;
        pSo.attackStats.CTP += iSo.criticalPercentagePoint;
        pSo.attackStats.CTD += iSo.criticalPowerPoint;
        //iSo.shadowGaugePoint 애는 나중에 만들면 될듯
    }
}
