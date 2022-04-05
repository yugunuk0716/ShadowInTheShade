using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamagable
{
    public Room currentRoom;

    public LayerMask whatIsHittable;

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnHit { get; set; }


    private bool isHit = false;
    public bool IsHit
    {
        get
        {
            return isHit;
        }
        set
        {

        }
    }

    public bool isDie = false;

    public float maxHP = 0f;
    private float currHP = 0f;

    private float currentT = 0f;
    private float lastHitT = 0f;
    private float hitCool = 1f;


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

    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.1f);

    private void Start()
    {
        currHP = maxHP;
        RoomManager.Instance.OnMoveRoomEvent.AddListener(() =>
        {
            currentRoom.doorList.ForEach(d => d.IsOpen = false);
        });

    }


    private void Update()
    {
        currentT = Time.time;

        if (Input.GetKeyDown(KeyCode.O))
        {
            DoorOpen();
        }

        if(currentT - lastHitT >= hitCool)
        {
            isHit = false;
        }
    }

    public void DoorOpen()
    {
        currentRoom.doorList.ForEach(d => d.IsOpen = true);
    }

    public void GetHit(int damage)
    {

        if (isDie || isHit )
            return;
        lastHitT = currentT;

        isHit = true;
       

        currHP -= damage;

        StartCoroutine(Blinking());
        StartCoroutine(StateRoutine());
        //move.rigid.velocity = Vector2.zero;

        CheckHp();

        OnHit?.Invoke();

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
        if (isHit || isDie)
            return;
        if (move == null)
        {
            print("왜 없음?");
            return;
        }
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
        if (currHP <= 0f)
        {
            isDie = true;
            StartCoroutine(Dead());
            OnDie?.Invoke();
        }
        
    }

    public IEnumerator Dead()
    {
        if (isDie.Equals(true))
        {
            Anim.SetTrigger("isDie");
            yield return null;
            
            //나중에 여기서 게임오버 패널 띄우는 함수 실행하면 될 듯
        }
    }

    

    private void OnParticleCollision(GameObject other)
    {
        if ((1 << other.layer & whatIsHittable) > 0)
        {
            GetHit(1);
            KnockBack(this.transform.position - other.transform.position, 2f, 0.3f);
            EffectManager.Instance.BloodEffect(EffectType.SLIME, 0.5f, 1f, 0.7f);
        }
    }

}
