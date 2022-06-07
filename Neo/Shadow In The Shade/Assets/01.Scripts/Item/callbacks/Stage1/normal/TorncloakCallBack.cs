using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorncloakCallBack : ItemCallBack
{
    public override void Start()
    {
        base.Start();
    }


    public override void ItemActiveCallBack()
    {
        GameManager.Instance.playerSO.ectStats.EVC += 3f;

        //회피 확률 3% 증가
    }

    public override void ItemSpecialCallBack()
    {
        //보스전 시작시 무효화
    }

    public override void ItemNestingCallBack()
    {
        //회피 확률 3% 증가
        GameManager.Instance.playerSO.ectStats.EVC += 3f;
        base.ItemNestingCallBack();

    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
