using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    //public ItemSO getItme;
    public GameObject imegeContent;

    public List<ItemSO> playerHasItems = new List<ItemSO>();

    private List<GameObject> itemUIObjs = new List<GameObject>();

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
        //iSo.shadowGaugePoint �ִ� ���߿� ����� �ɵ�



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
        for (int i = 0; i < playerHasItems.Count; i++)
        {
            if (item.isActived == false)
            {
                if (item != null)
                {
                    if (item.itemCallBack != null)
                    {
                        print("�Ⱥ���µ���");
                        GameObject itemObj = PoolManager.Instance.Pop(item.itemCallBack.name).gameObject;
                        itemObj.transform.SetParent(itemUIObjs[i].transform);
                        itemObj.name = item.name + "CallBackObj";
                    }
                    else
                    {
                        print("�ݹ� ������Ʈ�� ��");
                    }
                }
                else
                {
                    print("�׳� ������Ʈ�� �����");
                }
               item.isActived = true;
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
                    return;
                }
            }
        }
        GameManager.Instance.onPlayerGetItem?.Invoke();
        playerHasItems.Add(item);
        return;
    }
}
