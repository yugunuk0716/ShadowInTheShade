using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeFeedback : Feedback
{
    [SerializeField]
    private CinemachineVirtualCamera cinemachineCamObj;

    [SerializeField]
    private float shakeDuration;

    [SerializeField]
    private float shakePower;


    private CinemachineConfiner cinemachineCamConfiner;

    private CinemachineBasicMultiChannelPerlin cmPerlin;
    Tween camTween = null;

    public void Awake()
    {
        cinemachineCamConfiner = cinemachineCamObj.GetComponent<CinemachineConfiner>();
        cmPerlin = cinemachineCamObj.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CameraPerlinInit();
    }
    private void CameraPerlinInit()
    {
        if (cmPerlin == null)
            return;
        cmPerlin.m_AmplitudeGain = 0f;
    }


    public override void CompletePrevFeedback()
    {
        camTween.Kill();
    }

    public override void CreateFeedback()
    {
        if (cmPerlin != null)
        {
            cmPerlin.m_AmplitudeGain = shakePower;
        }
        else
        {
            print("ÆÞ¸° ¾øÀ½");
        }
        camTween = DOTween.To(() => cmPerlin.m_AmplitudeGain, value => cmPerlin.m_AmplitudeGain = value, 0, shakeDuration);
    }

}
