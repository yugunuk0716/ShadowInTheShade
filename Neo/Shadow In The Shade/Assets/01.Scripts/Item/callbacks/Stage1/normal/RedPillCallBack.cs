using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPillCallBack : ItemCallBack
{
    public override void Start()
    {
        base.Start();
    }


    public override void ItemActiveCallBack()
    {
        GameManager.Instance.playerSO.ectStats.PMH += 200;
        base.ItemActiveCallBack();
    }


    public override void ItemSpecialCallBack()
    {
        Debug.Log("?");
        GameManager.Instance.player.GetComponent<Player>().CurrHP += 100;
        base.ItemActiveCallBack();
    }

    public override void ItemNestingCallBack()
    {
        base.ItemNestingCallBack();

    }

    public override void Reset()
    {
       // throw new System.NotImplementedException();
    }

   
}
