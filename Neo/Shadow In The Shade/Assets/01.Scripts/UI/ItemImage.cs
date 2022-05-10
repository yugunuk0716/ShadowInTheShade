using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemImage : PoolableMono, IPointerEnterHandler, IPointerExitHandler
{
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

    public ItemSO itemSO;

    public override void Reset()
    {
       // throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemSO != null)
        {
            UIManager.Instance.ShowToolTip($"{itemSO.itemAbility} \n {itemSO.itemComment}", itemSO.itemSprite);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.CloseTooltip();
    }

   

    
}
