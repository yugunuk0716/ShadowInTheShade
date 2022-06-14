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
            Addingitem(itemUIObjs[0].transform, item);
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
                        Addingitem(itemUIObjs[itemUIObjs.Count - 1].transform, item);
                        StartCoroutine(CallGetItem());
                        return;
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
                    Addingitem(itemUIObjs[i].transform, item);
                    GameManager.Instance.onPlayerGetSameItem?.Invoke();
                    return;
                }
            }
        }
    }





    public void Addingitem(Transform parant,ItemSO item)
    {
        print(item.itemCallBack.name);
        GameObject itemObj = PoolManager.Instance.Pop(item.itemCallBack.name).gameObject;
        itemObj.SetActive(true);
        itemObj.transform.SetParent(parant);
        itemObj.name = item.name + "CallBackObj";
        playerHasItems.Add(item);
        item.isActived = true;
    }

    public IEnumerator CallGetItem()
    {
        yield return null;
        GameManager.Instance.onPlayerGetItem?.Invoke();
    }
}
