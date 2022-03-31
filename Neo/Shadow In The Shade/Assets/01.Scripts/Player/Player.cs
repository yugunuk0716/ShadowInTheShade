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


    public bool isHit = false;
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
    }

    public void DoorOpen()
    {
        currentRoom.doorList.ForEach(d => d.IsOpen = true);
    }

    public void GetHit(int damage)
    {
        if (isDie || isHit || lastHitT + hitCool > currentT)
            return;
        lastHitT = currentT;

        isHit = true;

        //float critical = Random.value;
        //bool isCritical = false;
        //if (critical <= GameManager.Instance.playerSO.attackStats.CTP)
        //{
        //    damage *= 2; //2배 데미지
        //    isCritical = true;
        //}


        currHP -= damage;

        StartCoroutine(Blinking());

        CheckHp();

        OnHit?.Invoke();

        //DamagePopup dPopup = PoolManager.Instance.Pop("DamagePopup") as DamagePopup;
        //dPopup.gameObject.SetActive(true);
        //dPopup?.SetText(damage, transform.position + new Vector3(0, 0.5f, 0f), isCritical);
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

    public virtual void CheckHp()
    {
        if (currHP <= 0f)
        {
            isDie = true;
            StartCoroutine(Dead());
            OnDie?.Invoke();
        }
        isHit = false;
    }

    public IEnumerator Dead()
    {
        if (isDie.Equals(true))
        {
            Anim.SetTrigger("isDie");
            yield return null;
            this.gameObject.SetActive(false);
            //나중에 여기서 게임오버 패널 띄우는 함수 실행하면 될 듯
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if ((1 << other.gameObject.layer & whatIsHittable) > 0)
        {
            GetHit(1);
            KnockBack(this.transform.position - other.transform.position, 2f, 0.3f);
            EffectManager.Instance.BloodEffect(EffectType.SLIME, 0.5f, 1f, 0.7f);
        }
    }

}
