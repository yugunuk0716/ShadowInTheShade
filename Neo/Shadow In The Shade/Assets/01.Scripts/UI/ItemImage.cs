using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemImage : PoolableMono, IPointerEnterHandler, IPointerExitHandler
{

    public List<Sprite> rarityOutLines = new List<Sprite>();

    public Image itemOutLine;

    public void SetRarity(int a)
    {
        itemOutLine.sprite = rarityOutLines[a];
    }

    private Image itemImg; 
    public Image ItemImg 
    {
        get
        { 
            if(itemImg == null)
                itemImg = GetComponent<Image>();
            return itemImg;
        }
      
    }

    private ItemSO itemSO;
    public ItemSO ItemSO
    {
        get 
        {
            return itemSO;
        }
        set 
        {
            itemSO = value;
            SetRarity((int)value.rarity);
        }
    }


    public override void Reset()
    {
       // throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemSO != null)
        {
            UIManager.Instance.ShowToolTip($"<b>{itemSO.itemName}</b> \n <size=5>     </size> \n {itemSO.itemAbility} \n {itemSO.itemComment}", itemSO.itemSprite);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.CloseTooltip();
    }

   

    
}
