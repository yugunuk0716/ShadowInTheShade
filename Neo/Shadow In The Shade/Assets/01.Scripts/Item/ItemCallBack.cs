using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemCallBack : PoolableMono
{
    public PlayerSO playerSO;
    public virtual void Start()
    {
        playerSO = GameManager.Instance.playerSO;
        GameManager.Instance.onPlayerGetItem.AddListener(ItemActiveCallBack);
        GameManager.Instance.onPlayerGetItem.AddListener(ItemSpecialCallBack);
    }
    public virtual void CallNesting()
    {
        GameManager.Instance.onPlayerGetSameItem.AddListener(ItemNestingCallBack);
    }

    public virtual void ItemActiveCallBack()
    {
        GameManager.Instance.onPlayerGetItem.RemoveListener(ItemActiveCallBack);
    }
    public abstract void ItemSpecialCallBack();
    public virtual void ItemNestingCallBack()
    {
        Debug.Log("ÁßÃ¸ ÇÏ°í »ç¶óÁü");
        GameManager.Instance.onPlayerGetSameItem.RemoveListener(ItemNestingCallBack);
    }
}
