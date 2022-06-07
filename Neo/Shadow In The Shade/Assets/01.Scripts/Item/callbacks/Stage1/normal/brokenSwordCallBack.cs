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
        GameManager.Instance.playerSO.attackStats.ATK += 20f;
    }

    public override void ItemSpecialCallBack()
    {

    }

    public override void ItemNestingCallBack()
    {
        GameManager.Instance.playerSO.attackStats.ATK += 10f;
        base.ItemNestingCallBack();


    }

    public override void Reset()
    {
       // throw new System.NotImplementedException();
    }
}
