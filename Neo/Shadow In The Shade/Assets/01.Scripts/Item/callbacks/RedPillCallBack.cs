using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPillCallBack : ItemCallBack
{
    public void Start()
    {
        GameManager.Instance.onPlayerGetItem.AddListener(CallBack);
    }


    public override void CallBack()
    {
        GameManager.Instance.player.GetComponent<Player>().CurrHP += 10f;
    }
}
