using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public  enum EffectType
{
    SLIME,
    BLOOD,
}


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

   
    public Image bloodImage;
    public CanvasGroup bloodImageCanvasGroup;

    private Sprite slimeBlood;
    private Sprite defaultBlood;

    private Sprite enemyHitEffect;
    private Sprite enemyHitCriticalEffect;

    public GameObject minimapCamObj;
    public CinemachineVirtualCamera cinemachineCamObj;
    [HideInInspector]
    public CinemachineConfiner cinemachineCamConfiner;
    [HideInInspector]
    public CinemachineVirtualCamera cinemachineCam;

    CinemachineBasicMultiChannelPerlin cmPerlin;
    //Tween camTween = null;
    Tween imageTween = null;

    private void Awake()
    {
        cinemachineCamObj = FindObjectOfType<CinemachineVirtualCamera>();
        bloodImageCanvasGroup = GameObject.Find("BloodEffectObj").GetComponent<CanvasGroup>();
        bloodImage = bloodImageCanvasGroup.GetComponent<Image>();
        slimeBlood = Resources.Load<Sprite>("slime");
        defaultBlood = Resources.Load<Sprite>("blood");
        minimapCamObj = GameObject.Find("MiniMapCamera");
        cinemachineCamConfiner = cinemachineCamObj.GetComponent<CinemachineConfiner>();
        cinemachineCam = cinemachineCamObj.GetComponent<CinemachineVirtualCamera>();
        cmPerlin = cinemachineCamObj.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CameraPerlinInit();

    }



   

    private void CameraPerlinInit()
    {
        if (cmPerlin == null)
            return;
        cmPerlin.m_AmplitudeGain = 0f;
    }

 

    public void BloodEffect(EffectType effectType = EffectType.BLOOD, float shakeDuration = 1f, float shakePower = 0.5f, float bloodEffectDuration = 1.5f, bool doShake = false)
    {
        Sprite sprite = null;

        switch (effectType)
        {
            case EffectType.SLIME:
                sprite = slimeBlood;
                break;
            case EffectType.BLOOD:
                sprite = defaultBlood;
                break;
          
        }

        if(sprite != null)
        {
            bloodImage.sprite = sprite;
        }



        if (imageTween != null && imageTween.IsActive())
        {
            imageTween.Kill();
        }
        
        if (bloodImageCanvasGroup != null)
        {
            bloodImageCanvasGroup.alpha = 1f;
        }
        else
        {
            print("블러드 이미지 없음");
        }

        imageTween = DOTween.To(() => bloodImageCanvasGroup.alpha, value => bloodImageCanvasGroup.alpha = value, 0f, bloodEffectDuration);
    }

   

    public void SetCamBound(Collider2D col)
    {
        cinemachineCamConfiner.m_BoundingShape2D = col;
    }

/*    public void CameraShake(float shakeDuration = 1f, float shakePower = 0.5f)
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
            print("펄린 없음");
        }
        camTween = DOTween.To(() => cmPerlin.m_AmplitudeGain, value => cmPerlin.m_AmplitudeGain = value, 0, shakeDuration);
    }
   */

}
