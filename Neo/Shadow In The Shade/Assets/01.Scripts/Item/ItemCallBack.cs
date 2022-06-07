using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemCallBack : PoolableMono
{
    public virtual void Start()
    {
        GameManager.Instance.onPlayerGetItem.AddListener(ItemSpecialCallBack);
        GameManager.Instance.onPlayerGetItem.AddListener(ItemActiveCallBack);
    }

    public virtual void CallNesting()
    {
        GameManager.Instance.onPlayerGetSameItem.AddListener(ItemNestingCallBack);
    }

    public abstract void ItemActiveCallBack();
    public abstract void ItemSpecialCallBack();
    public virtual void ItemNestingCallBack()
    {
        Debug.Log("ÁßÃ¸ ÇÏ°í »ç¶óÁü");
        GameManager.Instance.onPlayerGetSameItem.RemoveListener(ItemNestingCallBack);
    }
}
