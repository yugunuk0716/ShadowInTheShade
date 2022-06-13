using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenSwordCallBack : ItemCallBack
{
    public override void Start()
    {
        base.Start();
    }

    public override void ItemActiveCallBack()
    {
       // GameManager.Instance.playerSO.attackStats.ATK += 20f;
        GameManager.Instance.playerSO.PercentagePointStats.ATP += 20f;

    }

    public override void ItemSpecialCallBack()
    {

    }

    public override void ItemNestingCallBack()
    {
        // GameManager.Instance.playerSO.attackStats.ATK += 10f;
        GameManager.Instance.playerSO.PercentagePointStats.ATP += 20f;
        base.ItemNestingCallBack();


    }

    public override void Reset()
    {
       // throw new System.NotImplementedException();
    }
}
