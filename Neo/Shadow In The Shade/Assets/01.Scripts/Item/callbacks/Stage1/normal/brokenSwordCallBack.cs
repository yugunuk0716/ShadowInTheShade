using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenSwordCallBack : ItemCallBack
{
    void Start()
    {
        GameManager.Instance.onPlayerGetItem.AddListener(ItemActiveCallBack);
        GameManager.Instance.onPlayerGetItem.AddListener(ItemSpecialCallBack);
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
        GameManager.Instance.playerSO.attackStats.ATK += 20f;

    }

    public override void Reset()
    {
       // throw new System.NotImplementedException();
    }
}
