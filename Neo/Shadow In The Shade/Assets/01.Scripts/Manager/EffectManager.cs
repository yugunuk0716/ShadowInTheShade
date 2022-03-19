using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour
{

    private static EffectManager instance;
    public static EffectManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("EffectManager");
                obj.AddComponent<EffectManager>();
                instance = obj.GetComponent<EffectManager>();
            }

            return instance;
        }
    }

    float a = 1;
    public Image image;
    public GameObject bloodEffectObj;

    //Cinemachine Camera
    public CinemachineVirtualCamera cinemachineCamObj;
    [HideInInspector]
    public CinemachineConfiner cinemachineCamConfiner;
    [HideInInspector]
    public CinemachineVirtualCamera cinemachineCam;

    CinemachineBasicMultiChannelPerlin cmPerlin;
    Tween camTween = null;

    private void Awake()
    {
        cinemachineCamObj = FindObjectOfType<CinemachineVirtualCamera>();

        cinemachineCamConfiner = cinemachineCamObj.GetComponent<CinemachineConfiner>();
        cinemachineCam = cinemachineCamObj.GetComponent<CinemachineVirtualCamera>();
        cmPerlin = cinemachineCamObj.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Start()
    {

        //_cinemachineCamConfiner.m_BoundingShape2D = StageManager.Instance._rooms.Find((r) => r._isEntry)._camBound;


        //StartFadeOut();

    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        print("?");
        while (true)
        {
            a += 0.01f;
            image.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.01f);
            if (a >= 1)
                break;
        }


        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        a = 1f;
        while (true)
        {
            a -= 0.01f;
            image.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.01f);
            if (a <= 0)
                break;
        }

    }

    public void BloodEffect(float shakeDuration = 1f, float shakePower = 0.5f, float bloodEffectDuration = 1.5f)
    {
        if (camTween != null && camTween.IsActive())
        {
            camTween.Kill();
        }

        if (cmPerlin != null)
        {
            cmPerlin.m_AmplitudeGain = shakePower;
        }
        else
        {
            print("ÆÞ¸° ¾øÀ½");
        }
        camTween = DOTween.To(() => cmPerlin.m_AmplitudeGain, value => cmPerlin.m_AmplitudeGain = value, 0, shakeDuration);
        bloodEffectObj.SetActive(true);

        Invoke(nameof(SetObjectFalse), bloodEffectDuration);
    }

    public void SetObjectFalse()
    {
        bloodEffectObj.SetActive(false);
    }

    public void SetCamBound(Collider2D col)
    {
        cinemachineCamConfiner.m_BoundingShape2D = col;
    }

}
