using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamagable
{
 //   public Room currentRoom;

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent<float> OnHit { get; set; }

    private PlayerInput playerInput;
    private bool under50p = false;
    private PlayerSO so;
    //private PlayerDash playerDash;

    public PlayerInput PlayerInput
    {
        get
        {
            if (playerInput == null)
                playerInput = GetComponent<PlayerInput>();
            return playerInput;
        }
    }


    public bool IsHit
    {
        get
        {
            return PlayerInput.isHit;
        }
        set
        {
            PlayerInput.isHit = value;
        }
    }

    public bool IsDie
    {
        get
        {
            return PlayerInput.isDie;
        }
        set
        {
            PlayerInput.isDie = value;
        }
    }

    [SerializeField]
    private float currHP;
    public float CurrHP
    {
        get 
        {
            return currHP;
        }

        set 
        {
            currHP = value;
            UIManager.Instance.SetBar(currHP / (GameManager.Instance.playerSO.ectStats.PMH * 2));
        }
    }

    private float currentT = 0f;
    private float lastHitT = 0f;
    private readonly float hitCool = .5f;


    public PlayerMove move;

    private Animator anim;
    public Animator Anim
    {
        get
        {
            if (anim == null)
            {
                anim = GetComponentInChildren<Animator>();
            }

            return anim;
        }
    }

    private SpriteRenderer myRend;
    public SpriteRenderer MyRend
    {
        get
        {
            if (myRend == null)
            {
                myRend = GetComponentInChildren<SpriteRenderer>();
            }

            return myRend;
        }
    }

    private Rigidbody2D rigid;
    public Rigidbody2D Rigid
    {
        get
        {
            if (rigid == null)
            {
                rigid = GetComponentInChildren<Rigidbody2D>();
            }

            return rigid;
        }
    }

    public int LastHitObjNumber { get; set; } = 0;

    public bool isInvincibility = false;
    public bool isAttack = false;


    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.2f);



    private void Start()
    {
        CurrHP = GameManager.Instance.playerSO.ectStats.PMH * 2;
       // playerDash = GetComponent<PlayerDash>();
        OnHit.AddListener(GameManager.Instance.onPlayerHit.Invoke);
        
        so = GameManager.Instance.playerSO;
    }


    private void Update()
    {
        currentT = Time.time;

        if (Input.GetKeyDown(KeyCode.O))
        {
            StageManager.Instance.StageClear();
        }

        if(currentT - lastHitT >= hitCool)
        {
            IsHit = false;
        }

        if (currentT - lastHitT >= hitCool * 3f)
        {
            isInvincibility = false;
        }

        if (so.moveStats.HSP != 0)
        {
            if (currHP <= so.ectStats.PMH /2)
            {
                //적용
                if(!under50p)
                {
                    so.attackStats.ASD += so.moveStats.HSP;
                    under50p = true;
                }
            }
            else
            {
                if(under50p)
                {
                    so.attackStats.ASD -= so.moveStats.HSP;
                    under50p = false;
                }
                //적용풀기
            }
        }
    }




    public virtual void GetHit(float damage, int objNum)
    {
        if(GameManager.Instance.playerSO.ectStats.EVC != 0)
        {
            if(Random.Range(0f,100f) < GameManager.Instance.playerSO.ectStats.EVC)
            {
                //여기서 회피하면 나올 효과 써주면 될듯?
                print("응 못때리죠? 빡치죠? 화나죠?");
                return;
            }
        }
       
        if (damage > GameManager.Instance.playerSO.ectStats.PMH)
        {
            return;
        }

        if (IsDie || isInvincibility || isAttack)
            return;


        LastHitObjNumber = objNum;
        lastHitT = currentT;

        IsHit = true;
        isInvincibility = true;

        //Rigid.velocity = Vector2.zero;
        if (GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Human))
        {
            //print(damage);
            CurrHP -= damage;
        }
     

        StartCoroutine(Blinking());
        //StartCoroutine(StateRoutine());

        CheckHp();

        OnHit?.Invoke(damage);
        EffectManager.Instance.BloodEffect(EffectType.BLOOD, 0.5f, 1f, 0.7f);

        IsHit = false;

    }

    private IEnumerator StateRoutine()
    {
        //PlayerInputState oldState = GameManager.Instance.playerSO.playerInputState;
        //GameManager.Instance.playerSO.playerInputState = PlayerInputState.Hit;
      

            GameManager.Instance.timeScale = 0;
            yield return new WaitForSeconds(0.25f);
            GameManager.Instance.timeScale = 1;
    

        //GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
 
    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        if (IsDie || isInvincibility || isAttack )
            return;
        if (move == null) { }
      
        //move.KnockBack(direction, power, duration);
    }

    private IEnumerator Blinking()
    {
        while (true)
        {

            if (isInvincibility == false)
            {
                yield break;
            }

            yield return colorWait;
            MyRend.color = color_Trans;
            yield return colorWait;
            MyRend.color = Color.white;


        }
    }

    public void CheckHp()
    {
        if (CurrHP <= 0f)
        {
            print(CurrHP);
            IsDie = true;
            StartCoroutine(Dead());
            OnDie?.Invoke();
        }
        
    }

    public IEnumerator Dead()
    {
        if (IsDie.Equals(true))
        {
            Anim.SetTrigger("isDie");
            yield return null;

            //나중에 여기서 게임오버 패널 띄우는 함수 실행하면 될 듯

            SceneManager.LoadScene(0);
        }
    }

    

    //private void OnParticleCollision(GameObject other)
    //{
    //    if (((1 << other.layer) & whatIsHittable) > 0)
    //    {
    //        if (IsHit || IsDie)
    //            return;
    //        Rigid.velocity = Vector2.zero;
    //        print(other.transform.position - this.transform.position);
    //        KnockBack(other.transform.position - this.transform.position, 10f, 0.1f);
    //        GetHit(1);
    //        EffectManager.Instance.BloodEffect(EffectType.SLIME, 0.5f, 1f, 0.7f);
    //    }
    //}


  

}
