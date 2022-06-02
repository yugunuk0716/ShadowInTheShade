using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string statAbility;
    public string statComment;
    private Image statucSr;

    private void Awake()
    {
        statucSr = GetComponent<Image>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.ShowToolTip($"{statAbility} \n {statComment}", statucSr.sprite);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.CloseTooltip();
    }




}
