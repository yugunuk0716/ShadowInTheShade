 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slime_Shadow : Enemy, ITacklable
{
    private SpriteRenderer sr;
    public SpriteRenderer Sr
    {
        get 
        {
            if (sr == null)
                sr = GetComponent<SpriteRenderer>();
            return sr;
        }
    }

    private readonly float attackDistance = 2f;
    private readonly float chaseDistance = 7f;

    private bool isInvincibility = false;

    private Move_Chase chase = null;
    private Attack_Tackle attack = null;
    private Idle_Patrol idle = null;

    protected override void Awake()
    {
        idle = gameObject.AddComponent<Idle_Patrol>();
        dicState[EnemyState.Default] = idle;


        chase = gameObject.AddComponent<Move_Chase>();
        speed = 2f;

        dicState[EnemyState.Move] = chase;

        attack = gameObject.GetComponentInChildren<Attack_Tackle>();

        dicState[EnemyState.Attack] = attack;

        dicState[EnemyState.Die] = gameObject.AddComponent<Die_Default>();

        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public void SetTackle(bool on)
    {
        isAttack = on;
    }

    public void SetAttack()
    {
        attack.TackleEnd();
        attack.gameObject.SetActive(false);
        isInvincibility = true;
        Vector2 playerTrm = GameManager.Instance.player.position;
        Vector2 randValue = Vector2.zero;

        int randX = StageManager.Instance.currentRoom.transform.position.x > transform.position.x ? 1 : -1;
        int randY = StageManager.Instance.currentRoom.transform.position.y > transform.position.y ? 1 : -1;

        randValue.Set(Random.Range(2f, 3f) * randX + playerTrm.x, Random.Range(2f, 3f) * randY + playerTrm.y);

        Sr.DOFade(0, 1f).OnComplete(() => 
        {
            transform.DOMove(randValue, 2f).OnComplete(() =>
            {
                Sr.DOFade(1, 1f);
                attack.gameObject.SetActive(true);
                isInvincibility = false;
            }); 
        });

    }




    protected override void SetDefaultState(EnemyState state)
    {
        base.SetDefaultState(state);
    }

    protected override void SetState(EnemyState state)
    {
        base.SetState(state);
    }

    protected override void PlayState(EnemyState state)
    {
        base.PlayState(state);
    }

    protected override IEnumerator LifeTime()
    {
        yield return null;
        while (true)
        {
            if (IsHit)
            {
                yield return null;
                continue;
            }

            float dist = Vector2.Distance(transform.position, GameManager.Instance.player.position);
            if (!isAttack)
            {
                if (dist < chaseDistance)
                {
                    if (dist < attackDistance && attackCool + lastAttackTime < Time.time)
                    {
                        lastAttackTime = Time.time;
                        SetState(EnemyState.Attack);
                        attack.canAttack = true;
                        chase.canTrace = false;
                        idle.canMove = false;
                    }
                    else if (dist < chaseDistance)
                    {
                        chase.canTrace = true;
                        SetState(EnemyState.Move);
                    }
                }
                else
                {
                    SetState(EnemyState.Default);
                    idle.canMove = true;
                }
            }
            yield return base.LifeTime();
        }
    }

    public override void GetHit(float damage, int objNum)
    {
        if (isInvincibility)
            return;
        attack.TackleEnd();
        base.GetHit(damage, objNum);
    }

    protected override void CheckHP()
    {
        base.CheckHP();
    }

    public override IEnumerator Dead()
    {
        chase.speed = 0f;
        return base.Dead();
    }



    public override void Reset()
    {
        
        base.Reset();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            Gizmos.color = Color.white;
        }
    }
#endif
}
