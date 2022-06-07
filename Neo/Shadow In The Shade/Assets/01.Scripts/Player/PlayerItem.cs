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


        playerHasItems.Add(item);

        ItemImage image = PoolManager.Instance.Pop("ItemUIImage") as ItemImage;
        image.transform.SetParent(imegeContent.transform);
        image.ItemImg.sprite = item.itemSprite;
        image.name = item.name;
        image.ItemSO = item;
        //image.GetComponent<ItemImage>().SetRarity((int)item.rarity);
        itemUIObjs.Add(image.gameObject);
        ActiveItem();

        return;
    }

    public void ActiveItem()
    {
        Debug.Log("ActiveItem");
        PlayerSO pSo = GameManager.Instance.playerSO;
        for (int i = 0; i < playerHasItems.Count; i++)
        {
            if (playerHasItems[i].isActived == false)
            {
                /*   pSo.attackStats.ATK += playerHasItems[i].attackPoint;
                   pSo.ectStats.PMH += playerHasItems[i].maxHpPoint;
                   pSo.attackStats.ASD += playerHasItems[i].attackSpeedPoint;
                   pSo.moveStats.SPD += playerHasItems[i].moveSpeedPoint;
                   pSo.attackStats.CTP += playerHasItems[i].criticalPercentagePoint;
                   pSo.attackStats.CTD += playerHasItems[i].criticalPowerPoint;
   */
                if(playerHasItems[i] != null)
                {
                    if(playerHasItems[i].itemCallBack != null)
                    {
                        print("�Ⱥ���µ���");
                        GameObject itemObj = PoolManager.Instance.Pop(playerHasItems[i].itemCallBack.name).gameObject;
                        itemObj.transform.SetParent(itemUIObjs[i].transform);
                        itemObj.name = playerHasItems[i].name + "CallBackObj";
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

/*                // itemUIObjs[i].AddComponent<>();
                Debug.Log("ActiveItem");
               */
                playerHasItems[i].isActived = true;

            }
        }
        StartCoroutine(CallingItemCallBack());

        GameManager.Instance.playerSO = pSo;
    }

    public IEnumerator CallingItemCallBack()
    {
        yield return new WaitForSeconds(.02f);
        Debug.Log("Calling");
        GameManager.Instance.onPlayerGetItem.Invoke();
    }
}
