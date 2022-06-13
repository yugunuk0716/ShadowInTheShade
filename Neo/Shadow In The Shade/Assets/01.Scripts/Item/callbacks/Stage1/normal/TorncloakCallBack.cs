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
        // GameManager.Instance.playerSO.ectStats.EVC += 3f;
        playerSO.PercentagePointStats.STP += 40;
        ActiveStatPoint();
        //ȸ�� Ȯ�� 3% ����
    }

    public override void ItemSpecialCallBack()
    {
        //������ ���۽� ��ȿȭ
    }

    public override void ItemNestingCallBack()
    {
        //ȸ�� Ȯ�� 3% ����
        // GameManager.Instance.playerSO.ectStats.EVC += 3f;
        playerSO.PercentagePointStats.STP += 40;
        base.ItemNestingCallBack();

    }

    public override void Reset()
    {
       // throw new System.NotImplementedException();
    }

    public void ActiveStatPoint()
    {
        playerSO.attackStats.ATK += playerSO.mainStats.STR * 20f * playerSO.PercentagePointStats.STP / 100;

        playerSO.moveStats.SPD += playerSO.mainStats.DEX * .2f * playerSO.PercentagePointStats.STP / 100;
        playerSO.attackStats.ASD += playerSO.mainStats.DEX * 0.1f * playerSO.PercentagePointStats.STP / 100;

        playerSO.attackStats.CTP += playerSO.mainStats.AGI * 10f * playerSO.PercentagePointStats.STP / 100;

        playerSO.ectStats.PMH += playerSO.mainStats.SPL * 50f * playerSO.PercentagePointStats.STP / 100;
    }
}
