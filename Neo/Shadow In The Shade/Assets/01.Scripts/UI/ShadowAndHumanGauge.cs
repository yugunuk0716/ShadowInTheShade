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

    //private Sequence GaugeChangeSeq;

    public float shadowGaugeAmount = 0;
    public float humanGaugeAmount = 0;

    void Start()
    {
        /*        GaugeChangeSeq = DOTween.Sequence();
                shadowGaugeAmount = .5f;
                humanGaugeAmount = 1.5f;
                GaugeChangeSeq.Append(shadowGauge.rectTransform.DOScaleX(shadowGaugeAmount, .1f));
                GaugeChangeSeq.Join(humanGauge.rectTransform.DOScaleX(humanGaugeAmount, .1f)).OnComplete(() => gaugeState = GaugeState.Human);*/
        GameManager.Instance.onPlayerDash.AddListener(DashDecrease);
        StageManager.Instance.onBattleEnd.AddListener(() => 
        {
            if (gaugeState.Equals(GaugeState.Shadow))
            {
                GotoHuman();
            }
            shadowGaugeAmount = .5f;
            humanGaugeAmount = 1.5f;
            shadowGauge.rectTransform.DOScaleX(shadowGaugeAmount, .05f);
            humanGauge.rectTransform.DOScaleX(humanGaugeAmount, .05f);
        });
        shadowGaugeAmount = .5f;
        humanGaugeAmount = 1.5f;
        gaugeState = GaugeState.Shadow;
        DecreaseGauge(gaugeState);
        StartCoroutine(DefaultDecreaseGauge());
    }

    void Update()
    {

        if (gaugeState.Equals(GaugeState.Shadow))
        {

            if ((shadowGaugeAmount < 0.5f && humanGaugeAmount > 1.5f))
            {
                GotoHuman();
            }

        }
        else if (gaugeState.Equals(GaugeState.Human))
        {

            if ((humanGaugeAmount < 0.5f && shadowGaugeAmount > 1.5f))
            {
                GotoShadow();
            }

        }

    }

    public IEnumerator DefaultDecreaseGauge()
    {
        while (true)
        {
            yield return new WaitForSeconds(GameManager.Instance.defaultShadowGaugeSpeed);
            if (StageManager.Instance.isBattle)
            {
                DecreaseGauge(gaugeState);
            }
        }
    }

    public void DecreaseGauge(GaugeState state)
    {
        if (state.Equals(GaugeState.Shadow))
        {
            shadowGaugeAmount += -.01f;
            humanGaugeAmount += .01f;
        }
        else if (state.Equals(GaugeState.Human))
        {
            shadowGaugeAmount += .01f;
            humanGaugeAmount += -.01f;
        }

        shadowGauge.rectTransform.DOScaleX(shadowGaugeAmount, .05f);
        humanGauge.rectTransform.DOScaleX(humanGaugeAmount, .05f);
    }

    public void DashDecrease()
    {
        shadowGaugeAmount += -.1f;
        humanGaugeAmount += .1f;
    }

    public void GotoHuman()
    {
        gaugeState = GaugeState.Human;
        spliter.transform.localPosition = new Vector3(-120f, spliter.transform.localPosition.y);
          
        GameManager.Instance.OnPlayerChangingType?.Invoke();
       
    }

    public void GotoShadow()
    {
        gaugeState = GaugeState.Shadow;
        spliter.transform.localPosition = new Vector3(-274f, spliter.transform.localPosition.y);
        GameManager.Instance.OnPlayerChangingType?.Invoke();
    }
}
