using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShadowAndHumanGauge : MonoBehaviour
{
    [SerializeField]
    private Image spliter;

    [SerializeField]
    private Image shadowGague;

    [SerializeField]
    private Image humanGague;

    private Sequence GaugeChangeSeq;

    void Start()
    {
        GaugeChangeSeq = DOTween.Sequence();

        GaugeChangeSeq.Append(spliter.rectTransform.DOAnchorPos3DX(-130f, .3f));
        GaugeChangeSeq.Join(shadowGague.rectTransform.DOScaleX(1.4f,.3f));
        GaugeChangeSeq.Join(humanGague.rectTransform.DOScaleX(.6f,.3f));
    }

    void Update()
    {
        
    }
}
