using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TimeFreezeFeedback : Feedback
{
    [SerializeField]
    private float _freezeTimeDelay = 0.05f, _unFreezeTimeDelay = 0.02f;

    [SerializeField]
    [Range(0, 1f)]
    private float _timeFreezeValue = 0.2f;


    public override void CompletePrevFeedback()
    {
        if (TimeManager.Instance != null)
            TimeManager.Instance.ResetTimeScale();
    }

    public override void CreateFeedback()
    {
        // 0.05초후에 0.2로 타임을 조정했다가 다시 0.02초 후에 1로 변경한다.
        TimeManager.Instance.ModifyTimeScale(_timeFreezeValue, _freezeTimeDelay, () =>
        {
            TimeManager.Instance.ModifyTimeScale(1, _unFreezeTimeDelay);
        });
    }
}