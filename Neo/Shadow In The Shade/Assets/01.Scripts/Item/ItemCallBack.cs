using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemCallBack : PoolableMono
{
    public abstract void ItemActiveCallBack();
    public abstract void ItemSpecialCallBack();
    public abstract void ItemNestingCallBack();
}
