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
        //����� ���ӽð� 10% ����
    }

    public override void ItemSpecialCallBack()
    {
        //������ ���۽� ��ȿ
    }

    public override void ItemNestingCallBack()
    {
        //����� ���ӽð� 5% ����
        base.ItemNestingCallBack();

    }

    public override void Reset()
    {
       // throw new System.NotImplementedException();
    }
}
