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

        //ȸ�� Ȯ�� 3% ����
    }

    public override void ItemSpecialCallBack()
    {
        //������ ���۽� ��ȿȭ
    }

    public override void ItemNestingCallBack()
    {
        //ȸ�� Ȯ�� 3% ����
        GameManager.Instance.playerSO.ectStats.EVC += 3f;
        base.ItemNestingCallBack();

    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
