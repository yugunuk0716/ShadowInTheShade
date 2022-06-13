using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineCallBack : ItemCallBack
{
    public new void Start()
    {
        base.Start();
    }

    public override void ItemActiveCallBack()
    {
        //GameManager.Instance.playerSO.attackStats.ASD += 0.05f;
        playerSO.PercentagePointStats.SPP += 1;
        ActiveStats();
    }

    public override void ItemSpecialCallBack()
    {

    }

    public override void ItemNestingCallBack()
    {
        // GameManager.Instance.playerSO.attackStats.ASD += 0.04f;
        playerSO.PercentagePointStats.SPP += 1;
        base.ItemNestingCallBack();
    }

    public override void Reset()
    {
        //throw new System.NotImplementedException();
    }

    public void ActiveStats()
    {
        playerSO.attackStats.ASD += playerSO.mainStats.DEX * 0.1f * playerSO.PercentagePointStats.SPP / 100;
    }
}
