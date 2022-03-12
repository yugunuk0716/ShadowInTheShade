using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class StartButton : MonoBehaviour
{
    GameObject startObj;
    Vector3 originPos;

    private void Start()
    {
        startObj = GameObject.Find("StartObj");
        originPos = startObj.transform.localPosition;
        startObj.transform.localPosition += new Vector3(1000, 0, 0);
        GetComponent<Button>().onClick.AddListener(ClickStartButton);
    }

    private void ClickStartButton()
    {
        startObj.transform.DOLocalMove(originPos, 1f).SetEase(Ease.OutSine);
    }
}
