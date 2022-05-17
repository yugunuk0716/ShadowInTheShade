using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalineCallBack : ItemCallBack
{
    void Start()
    {
        GameManager.Instance.onPlayerGetItem.AddListener(ItemSpecialCallBack);
        GameManager.Instance.onPlayerGetItem.AddListener(ItemActiveCallBack);
    }

    public override void ItemActiveCallBack()
    {
        GameManager.Instance.playerSO.attackStats.ASD += 5;
    }

    public override void ItemSpecialCallBack()
    {

    }

    public override void ItemNestingCallBack()
    {
        GameManager.Instance.playerSO.attackStats.ASD += 4;
    }
}
