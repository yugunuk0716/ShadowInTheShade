using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirebloodCallBack : ItemCallBack
{
    public float heal = 0;

    void Start()
    {
        GameManager.Instance.onPlayerGetItem.AddListener(ItemSpecialCallBack);
        GameManager.Instance.onPlayerGetItem.AddListener(ItemActiveCallBack);
    }

    public override void ItemActiveCallBack()
    {
        heal += 2;
        GameManager.Instance.onEnemyHit.AddListener(HealingHp);
    }

    public override void ItemSpecialCallBack()
    {

    }

    public override void ItemNestingCallBack()
    {
        heal += 1;
       // HealingHp();
    }

    public void HealingHp()
    {
        GameManager.Instance.player.GetComponent<Player>().CurrHP += heal;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
