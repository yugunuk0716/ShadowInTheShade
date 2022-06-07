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
        GameManager.Instance.playerSO.attackStats.ASD += 0.05f;
    }

    public override void ItemSpecialCallBack()
    {

    }

    public override void ItemNestingCallBack()
    {
        GameManager.Instance.playerSO.attackStats.ASD += 0.04f;
        base.ItemNestingCallBack();
    }

    public override void Reset()
    {
        //throw new System.NotImplementedException();
    }
}
