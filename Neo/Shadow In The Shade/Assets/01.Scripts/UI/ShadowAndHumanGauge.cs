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
    private Image shadowGauge;

    [SerializeField]
    private Image humanGauge;

    private Sequence GaugeChangeSeq;

    void Start()
    {
        GaugeChangeSeq = DOTween.Sequence();

        GaugeChangeSeq.Append(spliter.rectTransform.DOAnchorPos3DX(-130f, .3f));
        GaugeChangeSeq.Join(shadowGauge.rectTransform.DOScaleX(1.4f,.3f));
        GaugeChangeSeq.Join(humanGauge.rectTransform.DOScaleX(.6f,.3f));
    }

    void Update()
    {
        
    }
}
