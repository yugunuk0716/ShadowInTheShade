using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireBloodCallBack : ItemCallBack
{
    public void Start()
    {
        GameManager.Instance.onPlayerGetItem.AddListener(ItemSpecialCallBack);
        GameManager.Instance.onPlayerGetItem.AddListener(ItemActiveCallBack);
    }

    public override void ItemActiveCallBack()
    {
        GameManager.Instance.playerSO.ectStats.APH += 2f;
    }

    public override void ItemSpecialCallBack()
    {

    }

    public override void ItemNestingCallBack()
    {
        GameManager.Instance.playerSO.ectStats.APH += 1f;
    }

    public override void Reset()
    {
        
    }
}
