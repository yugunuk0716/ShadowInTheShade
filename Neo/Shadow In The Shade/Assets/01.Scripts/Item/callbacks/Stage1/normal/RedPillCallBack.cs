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
        GameManager.Instance.playerSO.ectStats.PMH += 20f;
        base.ItemActiveCallBack();
    }


    public override void ItemSpecialCallBack()
    {
        Debug.Log("?");
        GameManager.Instance.player.GetComponent<Player>().CurrHP += 10f;
        UIManager.Instance.SetBar(
            GameManager.Instance.player.GetComponent<Player>().CurrHP / GameManager.Instance.playerSO.ectStats.PMH);
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
