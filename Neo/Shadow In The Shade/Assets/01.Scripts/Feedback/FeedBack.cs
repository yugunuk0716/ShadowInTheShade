
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    //�÷��̾��� ���ۿ� ���� �ǵ��
    public abstract void CreateFeedback();

    //���� �ǵ���� �����Ű��
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