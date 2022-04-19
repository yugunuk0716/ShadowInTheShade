using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple Timecontroller is running");
            Destroy(this); //������ �ϴ� �ı�
        }
        Instance = this;
    }

    public void ResetTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
    }

    public void ModifyTimeScale(float endTimeValue, float timeToWait, Action OnCompleteHandler = null)
    {
        StartCoroutine(TimeScaleCoroutine(endTimeValue, timeToWait, OnCompleteHandler));
    }

    IEnumerator TimeScaleCoroutine(float endTimeValue, float timeToWait, Action OnCompleteHandler = null)
    {
        //�ڷ�ƾ�� RealTime �� ���ð��� TimeScale�� ������� �ʴ´�.
        yield return new WaitForSecondsRealtime(timeToWait);
        Time.timeScale = endTimeValue;
        OnCompleteHandler?.Invoke();
    }
}
