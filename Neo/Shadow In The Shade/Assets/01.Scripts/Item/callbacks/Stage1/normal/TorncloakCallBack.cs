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

    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
