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
            feedback.CreateFeedback(); //새로운 피드백을 생성한다.
        }
    }

    private void FinishFeedback()
    {
        foreach (Feedback feedback in _feedbackToPlay)
        {
            feedback.CompletePrevFeedback(); //이전 피드백 종료시키고
        }
    }
}