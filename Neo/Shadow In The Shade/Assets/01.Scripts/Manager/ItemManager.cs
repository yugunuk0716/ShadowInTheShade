using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public enum Rarity
{
    Normal,
    Rare,
    Unique,
    Legendary
}

public class ItemManager : MonoBehaviour
{
    [Header("아이템 SO 리스트")]
    public List<ItemSO> items;

    public ItemSO PickItem(Rarity _rarity)
    {
        List<ItemSO> pickedItems = new List<ItemSO>();

        foreach(ItemSO a in items)
        {
            if(a.rarity.Equals(_rarity))
            {
                pickedItems.Add(a);
            }
        }

        return pickedItems[Random.Range(0, pickedItems.Count)];
    }

    public void AddingItem(ItemSO item)
    {
        GameManager.Instance.player.GetComponent<PlayerItem>().getItme = item;
    }
}
