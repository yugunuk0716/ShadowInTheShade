using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPillCallBack : ItemCallBack
{
    public void Start()
    {
        GameManager.Instance.onPlayerGetItem.AddListener(ItemSpecialCallBack);
        GameManager.Instance.onPlayerGetItem.AddListener(ItemActiveCallBack);
        Debug.Log("Adding");
    }

    public override void ItemActiveCallBack()
    {
        GameManager.Instance.playerSO.ectStats.PMH += 20f;
        base.ItemActiveCallBack();
    }


    public override void ItemSpecialCallBack()
    {
        Debug.Log("?");
        GameManager.Instance.player.GetComponent<Player>().CurrHP += 10f;
        UIManager.Instance.SetBar(
            GameManager.Instance.player.GetComponent<Player>().CurrHP / GameManager.Instance.playerSO.ectStats.PMH);
    }
}
