using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public ItemSO getItme;

    public List<ItemSO> playerHasItems = new List<ItemSO>();

    private void Update()
    {
        if(getItme != null)
        {
            AddingItem(getItme);
        }
    }

    public void AddingItem(ItemSO item)
    {
        PlayerSO pSo = GameManager.Instance.playerSO;

        pSo.attackStats.ATK += item.attackPoint;
        pSo.ectStats.PMH += item.maxHpPoint;
        pSo.attackStats.ASD += item.attackSpeedPoint;
        pSo.moveStats.SPD += item.moveSpeedPoint;
        pSo.attackStats.CTP += item.criticalPercentagePoint;
        pSo.attackStats.CTD += item.criticalPowerPoint;

        GameManager.Instance.playerSO = pSo;
        //iSo.shadowGaugePoint 애는 나중에 만들면 될듯
    }
}
