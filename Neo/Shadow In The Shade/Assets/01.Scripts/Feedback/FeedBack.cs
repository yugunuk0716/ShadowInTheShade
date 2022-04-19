
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    //플레이어의 동작에 따른 피드백
    public abstract void CreateFeedback();

    //이전 피드백을 종료시키기
    public abstract void CompletePrevFeedback();

    protected virtual void OnDestroy()
    {
        CompletePrevFeedback();
    }

    protected virtual void OnDisable()
    {
        CompletePrevFeedback();
    }
}