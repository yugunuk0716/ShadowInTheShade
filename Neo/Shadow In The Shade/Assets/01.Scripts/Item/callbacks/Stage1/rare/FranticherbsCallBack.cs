using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FranticherbsCallBack : ItemCallBack
{
    public override void Start()
    {
        base.Start();
    }

    public override void ItemActiveCallBack()
    {
        GameManager.Instance.playerSO.attackStats.KAP += 5f;
        base.ItemActiveCallBack();
    }

    public override void ItemNestingCallBack()
    {
        GameManager.Instance.playerSO.attackStats.KAP += 5f;
        base.ItemNestingCallBack();

    }

    public override void ItemSpecialCallBack()
    {

    }

    public override void Reset()
    {
    }
}
