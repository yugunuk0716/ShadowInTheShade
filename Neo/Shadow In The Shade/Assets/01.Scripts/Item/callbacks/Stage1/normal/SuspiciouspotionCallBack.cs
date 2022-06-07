using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspiciouspotionCallBack : ItemCallBack
{
    void Start()
    {
        GameManager.Instance.onPlayerGetItem.AddListener(ItemSpecialCallBack);
        GameManager.Instance.onPlayerGetItem.AddListener(ItemActiveCallBack);
    }

    public override void ItemActiveCallBack()
    {
        //����� ���ӽð� 10% ����
    }

    public override void ItemSpecialCallBack()
    {
        //������ ���۽� ��ȿ
    }

    public override void ItemNestingCallBack()
    {
        //����� ���ӽð� 5% ����
    }

    public override void Reset()
    {
       // throw new System.NotImplementedException();
    }
}
