using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public enum GaugeState
{
    Shadow,
    Human
}


public class ShadowAndHumanGauge : MonoBehaviour
{
    [SerializeField]
    private GaugeState gaugeState;

    [SerializeField]
    private Image spliter;

    [SerializeField]
    private Image shadowGauge;

    [SerializeField]
    private Image humanGauge;

    private Sequence GaugeChangeSeq;

    public float shadowGaugeAmount = 0;
    public float humanGaugeAmount = 0;

    void Start()
    {
        GaugeChangeSeq = DOTween.Sequence();
        shadowGaugeAmount = .5f;
        humanGaugeAmount = 1.5f;
        GaugeChangeSeq.Append(shadowGauge.rectTransform.DOScaleX(shadowGaugeAmount, .1f));
        GaugeChangeSeq.Join(humanGauge.rectTransform.DOScaleX(humanGaugeAmount, .1f)).OnComplete(() => gaugeState = GaugeState.Human);
        DecreaseGauge(gaugeState);
        StartCoroutine(DefaultDecreaseGauge());
    }

    void Update()
    {
        if (gaugeState.Equals(GaugeState.Shadow))
        {
            if ((shadowGaugeAmount < 0.5f && humanGaugeAmount > 1.5f))
            {
                gaugeState = GaugeState.Human;
                spliter.transform.DOLocalMoveX(-120, .3f);
            }
        }
        else if (gaugeState.Equals(GaugeState.Human))
        {
            if ((humanGaugeAmount < 0.5f && shadowGaugeAmount > 1.5f))
            {
                gaugeState = GaugeState.Shadow;
                spliter.transform.DOLocalMoveX(-274f, .3f);
            }
        }
    }

    public IEnumerator DefaultDecreaseGauge()
    {
        while (true)
        {
            yield return new WaitForSeconds(GameManager.Instance.defaultShadowGaugeSpeed);
            DecreaseGauge(gaugeState);
        }
    }

    public void DecreaseGauge(GaugeState state)
    {
        if (state.Equals(GaugeState.Shadow))
        {
            shadowGaugeAmount += -.1f;
            humanGaugeAmount += .1f;
        }
        else if (state.Equals(GaugeState.Human))
        {
            shadowGaugeAmount += .1f;
            humanGaugeAmount += -.1f;
        }

        shadowGauge.rectTransform.DOScaleX(shadowGaugeAmount, .05f);
        humanGauge.rectTransform.DOScaleX(humanGaugeAmount, .05f);
    }
}
