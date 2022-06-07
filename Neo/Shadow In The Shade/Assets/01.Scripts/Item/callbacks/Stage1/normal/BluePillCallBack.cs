using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePillCallBack : ItemCallBack
{
    public new void Start()
    {
        base.Start();
    }




    public override void ItemActiveCallBack()
    {
        GameManager.Instance.playerSO.ectStats.DPD += 1.1f;
    }

    public override void ItemNestingCallBack()
    {
        GameManager.Instance.playerSO.ectStats.DPD += 0.05f;
        base.ItemNestingCallBack();
    }

    public override void ItemSpecialCallBack()
    {
    }

    public override void Reset()
    {
    }
}
