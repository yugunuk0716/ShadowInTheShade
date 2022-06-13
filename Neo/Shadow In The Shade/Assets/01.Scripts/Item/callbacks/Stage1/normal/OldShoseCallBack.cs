using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldShoseCallBack : ItemCallBack
{
    public override void Start()
    {
        base.Start();
    }


    public override void ItemActiveCallBack()
    {
        GameManager.Instance.playerSO.moveStats.SPD += .4f;
        base.ItemActiveCallBack();
    }

    public override void ItemSpecialCallBack()
    {

    }

    public override void ItemNestingCallBack()
    {
        GameManager.Instance.playerSO.moveStats.SPD += .2f;
        base.ItemNestingCallBack();

    }

    public override void Reset()
    {
        //throw new System.NotImplementedException();
    }
}
