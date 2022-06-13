using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireBloodCallBack : ItemCallBack
{
    public override void Start()
    {
        base.Start();
    }

    public override void ItemActiveCallBack()
    {
        GameManager.Instance.playerSO.ectStats.APH += 20f;
        base.ItemActiveCallBack();
    }

    public override void ItemSpecialCallBack()
    {

    }

    public override void ItemNestingCallBack()
    {
        GameManager.Instance.playerSO.ectStats.APH += 10f;
        base.ItemNestingCallBack();

    }

    public override void Reset()
    {
        
    }
}
