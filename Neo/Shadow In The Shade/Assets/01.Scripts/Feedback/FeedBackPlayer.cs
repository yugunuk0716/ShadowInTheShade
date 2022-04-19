using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackPlayer : MonoBehaviour
{
    [SerializeField]
    private List<Feedback> _feedbackToPlay = null;

    public void PlayFeedback()
    {
        FinishFeedback();
        foreach (Feedback feedback in _feedbackToPlay)
        {
            feedback.CreateFeedback(); //���ο� �ǵ���� �����Ѵ�.
        }
    }

    private void FinishFeedback()
    {
        foreach (Feedback feedback in _feedbackToPlay)
        {
            feedback.CompletePrevFeedback(); //���� �ǵ�� �����Ű��
        }
    }
}