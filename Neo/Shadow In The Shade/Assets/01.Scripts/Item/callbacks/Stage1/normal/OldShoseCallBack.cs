using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldShoseCallBack : ItemCallBack
{
    void Start()
    {
        GameManager.Instance.onPlayerGetItem.AddListener(ItemSpecialCallBack);
        GameManager.Instance.onPlayerGetItem.AddListener(ItemActiveCallBack);
    }

    public override void ItemActiveCallBack()
    {
        GameManager.Instance.playerSO.moveStats.SPD += .4f;
    }

    public override void ItemSpecialCallBack()
    {

    }

    public override void ItemNestingCallBack()
    {
        GameManager.Instance.playerSO.moveStats.SPD += .2f;
    }
}
