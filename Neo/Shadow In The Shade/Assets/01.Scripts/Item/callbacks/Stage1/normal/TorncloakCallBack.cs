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
        //ȸ�� Ȯ�� 3% ����
    }

    public override void ItemSpecialCallBack()
    {
        //������ ���۽� ��ȿȭ
    }

    public override void ItemNestingCallBack()
    {
        //ȸ�� Ȯ�� 3% ����
    }
}
