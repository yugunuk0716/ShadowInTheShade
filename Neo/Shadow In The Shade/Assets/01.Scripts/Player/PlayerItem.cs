using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    //public ItemSO getItme;
    public GameObject imegeContent;

    public List<ItemSO> playerHasItems = new List<ItemSO>();

    public List<GameObject> itemUIObjs = new List<GameObject>();

    /*    private void Update()
        {
            if(getItme != null)
            {
                AddingItem(getItme);
            }
        }*/

    public void AddingItem(ItemSO item)
    {
        /* PlayerSO pSo = GameManager.Instance.playerSO;

         pSo.attackStats.ATK += item.attackPoint;
         pSo.ectStats.PMH += item.maxHpPoint;
         pSo.attackStats.ASD += item.attackSpeedPoint;
         pSo.moveStats.SPD += item.moveSpeedPoint;
         pSo.attackStats.CTP += item.criticalPercentagePoint;
         pSo.attackStats.CTD += item.criticalPowerPoint;

         GameManager.Instance.playerSO = pSo;*/
        //iSo.shadowGaugePoint 애는 나중에 만들면 될듯



        ItemImage image = PoolManager.Instance.Pop("ItemUIImage") as ItemImage;
        image.transform.SetParent(imegeContent.transform);
        image.ItemImg.sprite = item.itemSprite;
        image.name = item.name;
        image.ItemSO = item;
        //image.GetComponent<ItemImage>().SetRarity((int)item.rarity);
        itemUIObjs.Add(image.gameObject);


        ActiveItem(item);

        return;
    }

    public void ActiveItem(ItemSO item)
    {
        if (playerHasItems.Count == 0)
        {
            GameObject itemObj = PoolManager.Instance.Pop(item.itemCallBack.name).gameObject;
            itemObj.transform.SetParent(itemUIObjs[0].transform);
            itemObj.name = item.name + "CallBackObj";
            playerHasItems.Add(item);
            item.isActived = true;
            StartCoroutine(CallGetItem());
            return;
        }

        for (int i = 0; i < playerHasItems.Count; i++)
        {
            if (item.isActived == false)
            {
                if (item != null)
                {
                    if (item.itemCallBack != null)
                    {
                        print("안비었는데용");
                        GameObject itemObj = PoolManager.Instance.Pop(item.itemCallBack.name).gameObject;
                        itemObj.SetActive(true);
                        itemObj.transform.SetParent(itemUIObjs[itemUIObjs.Count -1].transform);
                        itemObj.name = item.name + "CallBackObj";
                        playerHasItems.Add(item);
                        item.isActived = true;

                    }
                    else
                    {
                        print("콜백 오브젝트가 빔");
                    }
                }
                else
                {
                    print("그냥 오브젝트가 읎어요");
                }
            }
            else
            {
                if (item.name.Equals(playerHasItems[i].name))
                {
                    GameObject itemObj = PoolManager.Instance.Pop(item.itemCallBack.name).gameObject;
                    itemObj.transform.SetParent(itemUIObjs[i].transform);
                    itemObj.name = item.name + "CallBackObj";
                    itemObj.GetComponent<ItemCallBack>().CallNesting();
                    item.isActived = true;
                    GameManager.Instance.onPlayerGetSameItem?.Invoke();
                    playerHasItems.Add(item);
                    return;
                }
            }
        }

        StartCoroutine(CallGetItem());

        return;
    }

    public IEnumerator CallGetItem()
    {
        yield return null;
        GameManager.Instance.onPlayerGetItem?.Invoke();
    }
}
