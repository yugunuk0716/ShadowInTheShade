using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPillCallBack : ItemCallBack
{
    public void Start()
    {
        GameManager.Instance.onPlayerGetItem.AddListener(ItemSpecialCallBack);
        Debug.Log("Adding");
    }


    public override void ItemSpecialCallBack()
    {
        Debug.Log("?");
        GameManager.Instance.player.GetComponent<Player>().CurrHP += 10f;
        base.ItemSpecialCallBack();
        UIManager.Instance.SetBar(
            GameManager.Instance.player.GetComponent<Player>().CurrHP / GameManager.Instance.playerSO.ectStats.PMH);
    }
}
