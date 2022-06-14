using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspiciouspotionCallBack : ItemCallBack
{
    public override void Start()
    {
        base.Start();
    }


    public override void ItemActiveCallBack()
    {
        //디버프 지속시간 10% 감소
        playerSO.attackStats.BSP += 2f;
        base.ItemActiveCallBack();
    }

    public override void ItemSpecialCallBack()
    {
        //보스전 시작시 무효
    }

    public override void ItemNestingCallBack()
    {
        //디버프 지속시간 5% 감소
        playerSO.attackStats.BSP += 2f;
        base.ItemNestingCallBack();

    }

    public override void Reset()
    {
       // throw new System.NotImplementedException();
    }
}
