using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicepuddingCallBack : ItemCallBack
{
    public override void Start()
    {
        base.Start();
    }


    public override void ItemActiveCallBack()
    {
        GameManager.Instance.playerSO.moveStats.DCT -= 1f;
    }

    public override void ItemNestingCallBack()
    {
        GameManager.Instance.playerSO.moveStats.DCT -= .5f;
        base.ItemNestingCallBack();

    }

    public override void ItemSpecialCallBack()
    {
    }

    public override void Reset()
    {
    }
}
