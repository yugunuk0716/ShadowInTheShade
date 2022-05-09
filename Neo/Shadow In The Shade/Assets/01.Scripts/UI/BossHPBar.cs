using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossHPBar : MonoBehaviour
{
 
    public Image mk1HPFill;
    public Image mk2HPFill;
    public Image mk3HPFill;
    public Image mk1HPFill_White;
    public Image mk2HPFill_White;
    public Image mk3HPFill_White;

    public float curMk1HP;
    public float CurMk1HP 
    {
        get 
        {
            return curMk1HP; 
        } 
        set 
        {
            curMk1HP = value;
            mk1HPFill.fillAmount = curMk1HP / 25000f;
            mk1HPFill_White.DOFillAmount(mk1HPFill.fillAmount, .3f).SetEase(Ease.Linear).SetDelay(.5f);
            curMk1HP = 0;
        }
    }
    public float curMk2HP;
    public float CurMk2HP
    {
        get
        {
            return curMk2HP;
        }
        set
        {
            curMk2HP = value;
            mk2HPFill.fillAmount = curMk2HP / 12500f;
            mk2HPFill_White.DOFillAmount(mk2HPFill.fillAmount, .3f).SetEase(Ease.Linear).SetDelay(.5f);
            curMk2HP = 0;
        }
    }
    public float curMk3HP;
    public float CurMk3HP
    {
        get
        {
            return curMk3HP;
        }
        set
        {
            curMk3HP = value;
            mk3HPFill.fillAmount = curMk3HP / 2500f;
            mk3HPFill_White.DOFillAmount(mk3HPFill.fillAmount, .3f).SetEase(Ease.Linear).SetDelay(.5f);
            curMk3HP = 0;
        }
    }
    
    public void AddBossHp(float hp, DiceType type)
    {
        if(type == DiceType.Mk1)
        {
            CurMk1HP += hp;
        }
        else if(type == DiceType.Mk2)
        {
            CurMk2HP += hp;
        }
        else
        {
            CurMk3HP += hp;
        }

        
    }


    void Start()
    {
        GameManager.Instance.onBossHpSend.AddListener(AddBossHp);
    }

    void Update()
    {
        
    }
}
