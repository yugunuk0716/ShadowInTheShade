using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerStatsPopup : MonoBehaviour
{
    [SerializeField]
    private GameObject statePopup;

    private Vector2 originPos = new Vector2();

    private bool isPopupOn = false;

    private void Start()
    {
        statePopup.transform.position = new Vector3(-300f, statePopup.transform.position.y);
        originPos = statePopup.transform.position;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            TogglePopup();
        }
    }


    public void TogglePopup()
    {
        if(isPopupOn)
        {
            statePopup.transform.DOMoveX(originPos.x, .5f).SetEase(Ease.Linear);
        }
        else
        {
            statePopup.transform.DOMoveX(245f, .5f).SetEase(Ease.InExpo);
        }

        isPopupOn = !isPopupOn;
    }
}
