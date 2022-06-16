using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowExtractCallBack : ItemCallBack
{
    public override void Start()
    {
        base.Start();
    }

    public override void ItemActiveCallBack()
    {
        GameManager.Instance.playerSO.moveStats.HSP += .5f;
        base.ItemActiveCallBack();
    }

    public override void ItemNestingCallBack()
    {
        base.ItemNestingCallBack();

    }

    public override void ItemSpecialCallBack()
    {
    }

    public override void Reset()
    {
    }
}
