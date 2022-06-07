using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBatCallBack : ItemCallBack
{
    public override void Start()
    {
        base.Start();
    }

    public override void ItemActiveCallBack()
    {
        GameManager.Instance.playerSO.attackStats.CTP += 5;
    }

    public override void ItemSpecialCallBack()
    {
    }

    public override void ItemNestingCallBack()
    {
        GameManager.Instance.playerSO.attackStats.CTP += 3;
        base.ItemNestingCallBack();

    }

    public override void Reset()
    {
      //  throw new System.NotImplementedException();
    }
}
