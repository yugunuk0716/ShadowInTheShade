using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamagable
{
 //   public Room currentRoom;

    public LayerMask whatIsHittable;

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnHit { get; set; }

    private PlayerInput playerInput;
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

    public float maxHP = 0f;

    private float currHP = 0f;
    public float CurrHP
    {
        get 
        {
            return currHP;
        }

        set 
        {
            currHP = value;
            UIManager.Instance.SetBar(currHP / maxHP);
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

    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.1f);



    private void Start()
    {
        CurrHP = maxHP;
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
    }


   

    public void GetHit(int damage)
    {

        if (IsDie || IsHit)
            return;
        lastHitT = currentT;

        IsHit = true;
       

        CurrHP -= damage;

        StartCoroutine(Blinking());
        StartCoroutine(StateRoutine());

        CheckHp();

        OnHit?.Invoke();
        EffectManager.Instance.BloodEffect(EffectType.SLIME, 0.5f, 1f, 0.7f);

    }

    private IEnumerator StateRoutine()
    {
        //PlayerInputState oldState = GameManager.Instance.playerSO.playerInputState;
        //GameManager.Instance.playerSO.playerInputState = PlayerInputState.Hit;
        GameManager.Instance.timeScale = 0;
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.timeScale = 1;

        //GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
 
    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        if (IsHit || IsDie)
            return;
        if (move == null)
      
        move.KnockBack(direction, power, duration);
    }

    private IEnumerator Blinking()
    {
        MyRend.color = color_Trans;
        yield return colorWait;
        MyRend.color = Color.white;
    }

    public void CheckHp()
    {
        if (CurrHP <= 0f)
        {
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
