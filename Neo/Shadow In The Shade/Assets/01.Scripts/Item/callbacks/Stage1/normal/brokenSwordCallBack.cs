using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenSwordCallBack : ItemCallBack
{
    public override void Start()
    {
        base.Start();
    }

    public override void ItemActiveCallBack()
    {
       // GameManager.Instance.playerSO.attackStats.ATK += 20f;
        GameManager.Instance.playerSO.PercentagePointStats.ATP += 20f;
        ActiveATP();
        base.ItemActiveCallBack();
    }

    public override void ItemSpecialCallBack()
    {

    }

    public override void ItemNestingCallBack()
    {
        // GameManager.Instance.playerSO.attackStats.ATK += 10f;
        GameManager.Instance.playerSO.PercentagePointStats.ATP += 20f;
        base.ItemNestingCallBack();


    }

    public override void Reset()
    {
       // throw new System.NotImplementedException();
    }

    public void ActiveATP()
    {
        PlayerSO pso = GameManager.Instance.playerSO;

        pso.attackStats.ATK += pso.attackStats.ATK * pso.PercentagePointStats.ATP / 100;
    }
}
