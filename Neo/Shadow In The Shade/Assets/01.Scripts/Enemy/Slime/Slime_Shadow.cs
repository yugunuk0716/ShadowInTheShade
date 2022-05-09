 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slime_Shadow : Enemy, ITacklable
{
    private List<PhaseInfo> phaseInfoList = new List<PhaseInfo>();



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
    private readonly float chaseDistance = 5f;

    private Move_Chase chase = null;
    private Attack_Tackle attack = null;


    private readonly WaitForSeconds halfSecWait = new WaitForSeconds(0.5f);
    private readonly WaitForSeconds oneSecWait = new WaitForSeconds(1f);
    private readonly WaitForSeconds threeSecWait = new WaitForSeconds(3f);




    protected override void Awake()
    {
        dicState[EnemyState.Default] = gameObject.AddComponent<Idle_Patrol>();


        chase = gameObject.AddComponent<Move_Chase>();
        chase.speed = 2f;

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
        Vector2 playerTrm = GameManager.Instance.player.position;
        Vector2 randValue = Vector2.zero;

        int randX = Random.Range(0, 2) == 0 ? 1 : -1;
        int randY = Random.Range(0, 2) == 0 ? 1 : -1;

        randValue.Set(Random.Range(3f, 3.5f) * randX + playerTrm.x, Random.Range(3f, 3.5f) * randY + playerTrm.y);

        //if (Random.Range(0, 2) == 0)
        //{
        //    randValue.Set(randValue.x * -1, randValue.y);
        //}

        //if (Random.Range(0, 2) == 0)
        //{
        //    randValue.Set(randValue.x, randValue.y * -1);
        //}

        Sr.DOFade(0, 1f).OnComplete(() => 
        {
            transform.DOMove(randValue, 2f).OnComplete(() =>
            {
                Sr.DOFade(1, 1f);
                attack.gameObject.SetActive(true);
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
                    if (dist > attackDistance)
                    {
                        SetState(EnemyState.Move);
                    }

                    if (dist < attackDistance && attackCool + lastAttackTime < Time.time)
                    {
                        lastAttackTime = Time.time;
                        SetState(EnemyState.Attack);
                    }
                }
                else
                {
                    SetState(EnemyState.Default);
                }
            }
            yield return base.LifeTime();
        }
    }


    public override void GetHit(float damage)
    {
        base.GetHit(damage);
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
