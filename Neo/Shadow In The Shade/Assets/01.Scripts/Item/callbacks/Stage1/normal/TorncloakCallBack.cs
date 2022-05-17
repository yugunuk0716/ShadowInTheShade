using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorncloakCallBack : ItemCallBack
{
    void Start()
    {
        GameManager.Instance.onPlayerGetItem.AddListener(ItemSpecialCallBack);
        GameManager.Instance.onPlayerGetItem.AddListener(ItemActiveCallBack);
    }

    public override void ItemActiveCallBack()
    {
        //회피 확률 3% 증가
    }

    public override void ItemSpecialCallBack()
    {
        //보스전 시작시 무효화
    }

    public override void ItemNestingCallBack()
    {
        //회피 확률 3% 증가
    }
}
