using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePillar : PoolableMono, IDamagable
{

    public LayerMask whatIsHealable;

    [HideInInspector]
    public AudioClip slimeHitClip;


    private float currHP = 2;
    public float CurrHP
    {
        get
        {
            return currHP;
        }

        set
        {
            currHP = value;
            CheckHP();
        }
    }

    private bool isHit = false;
    public bool IsHit
    {
        get
        {
            return isHit;
        }
        set
        {
            isHit = value;
        }
    }

    private SpriteRenderer myRend;
    protected SpriteRenderer MyRend
    {
        get
        {
            if (myRend == null)
            {
                myRend = GetComponent<SpriteRenderer>();
            }

            return myRend;
        }
    }

    protected float hitCool = 0.5f;
    protected float lastHitTime = 0f;

    private readonly int healAmount = 2;
    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.1f);

    private void Awake()
    {
        slimeHitClip = Resources.Load<AudioClip>("Sounds/SlimeHit");
    }


    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((1 << collision.gameObject.layer & whatIsHealable) > 0)
        {
            Boss_Dice dice = collision.gameObject.GetComponent<Boss_Dice>();
            if(dice != null)
            {
                dice.CurrHP += healAmount;
                PoolManager.Instance.Push(this);
            }

            Boss_Dice_Mk2 mk2 = collision.gameObject.GetComponent<Boss_Dice_Mk2>();
            if(mk2 != null)
            {
                mk2.CurrHP += healAmount;
                PoolManager.Instance.Push(this);
            }
        }
    }

    protected virtual void CheckHP()
    {
        if (currHP <= 0)
        {
            PoolManager.Instance.Push(this);
        }
    }

    protected IEnumerator Blinking()
    {
        MyRend.color = color_Trans;
        yield return colorWait;
        MyRend.color = Color.white;
    }

    public virtual void GetHit(float damage)
    {
        if (isHit)
            return;

        isHit = true;
        lastHitTime = Time.time;
        float critical = Random.value;
        bool isCritical = false;
        if (critical <= GameManager.Instance.playerSO.attackStats.CTP)
        {
            damage *= 2; //2배 데미지
            isCritical = true;
        }


        SoundManager.Instance.GetAudioSource(slimeHitClip, false, SoundManager.Instance.BaseVolume).Play();
        currHP -= damage;

        StartCoroutine(Blinking());

        CheckHP();

        DamagePopup dPopup = PoolManager.Instance.Pop("DamagePopup") as DamagePopup;
        dPopup.gameObject.SetActive(true);
        dPopup?.SetText(damage, transform.position + new Vector3(0, 0.5f, 0f), isCritical);




    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        
    }

    public override void Reset()
    {

    }

}
