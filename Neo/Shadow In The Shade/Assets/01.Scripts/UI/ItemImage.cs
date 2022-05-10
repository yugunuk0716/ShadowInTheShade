using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImage : PoolableMono
{
    public List<Sprite> rarityOutLines = new List<Sprite>();

    public Image itemOutLine;

    public void SetRarity(int a)
    {
        itemOutLine.sprite = rarityOutLines[a];
    }

    public override void Reset()
    {

    }
}
