using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoinredbranchCallBack : ItemCallBack
{
    void Start()
    {
        GameManager.Instance.onPlayerGetItem.AddListener(ItemSpecialCallBack);
        GameManager.Instance.onPlayerGetItem.AddListener(ItemActiveCallBack);
    }

    public override void ItemActiveCallBack()
    {

    }

    public override void ItemSpecialCallBack()
    {

    }

    public override void ItemNestingCallBack()
    {

    }

    public override void Reset()
    {
       // throw new System.NotImplementedException();
    }
}
