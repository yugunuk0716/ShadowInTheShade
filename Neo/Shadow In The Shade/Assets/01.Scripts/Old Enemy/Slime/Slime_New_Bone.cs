using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Slime_New_Bone : Enemy, ITacklable
//{

//    private readonly float attackDistance = 2f;
//    private readonly float chaseDistance = 5f;
//    private int hitCount = 0;

//    private Move_Chase chase = null;
//    private Attack_Tackle attack = null;
//    private Idle_Patrol idle = null;

//    float defaultDamage;
//    readonly int damageIncreaseAmout = 2;



//    protected override void Awake()
//    {
//        idle = gameObject.AddComponent<Idle_Patrol>();
//        dicBehaviorState[EnemyBehaviorState.Idle] = idle;

//        chase = gameObject.AddComponent<Move_Chase>();
//        dicBehaviorState[EnemyBehaviorState.Chase] = chase;

//        attack = gameObject.GetComponentInChildren<Attack_Tackle>();

//        dicBehaviorState[EnemyBehaviorState.Attack] = attack;

//        dicConditionState[EnemyConditionState.Die] = gameObject.AddComponent<Die_Default>();

//        defaultDamage = enemyData.damage;
//        base.Awake();
//    }

//    public void OnEnable()
//    {
//        SetBehaviorState(EnemyBehaviorState.Idle);
//        StartCoroutine(LifeTime());
//    }

//    /*   protected override void Start()
//       {
//           base.Start();
//       }

//       protected override void OnEnable()
//       {
//           base.OnEnable();
//       }
//   */
//    /*    public void SetTackle(bool on)
//        {
//            isAttack = on;
//        }*/

//    public void SetAttack()
//    {

//        attack.TackleEnd();
//    }

   
//  /*  protected override void SetDefaultState(OldEnemyState state)
//    {
//        base.SetDefaultState(state);
//    }

//    protected override void SetState(OldEnemyState state)
//    {
//        base.SetState(state);
//    }

//    protected override void PlayState(OldEnemyState state)
//    {
//        base.PlayState(state);
//    }*/

//    protected override IEnumerator LifeTime()
//    {
//        yield return null;
//        while (true)
//        {
//            float dist = Vector2.Distance(transform.position, GameManager.Instance.player.position);

//            if (IsHit || isDie)
//            {
//                yield return null;
//                continue;
//            }
//            if (!isAttack)
//            {
//                if (dist < chaseDistance)
//                {
//                    if (dist < attackDistance/* && attackCool + lastAttackTime < Time.time*/)
//                    {
//                        /*lastAttackTime = Time.time;
//                        SetState(OldEnemyState.Attack);
//                        attack.canAttack = true;
//                        chase.canTrace = false;
//                        idle.canMove = false;*/
//                    }
//                    else if (dist < chaseDistance)
//                    {
//                        chase.canTrace = true;
//                        SetBehaviorState(EnemyBehaviorState.Chase);
//                    }
//                }
//                else
//                {
//                    SetBehaviorState(EnemyBehaviorState.Idle);
//                    idle.canMove = true;
//                }
//            }
          
            
//            yield return base.LifeTime();
//        }
//    }

//    public override void GetHit(float damage, int objNum)
//    {
//        hitCount++;
//        attack.TackleEnd();
//        base.GetHit(damage, objNum);
//    }

//    protected override void CheckHP()
//    {

//    /*    if (hitCount > 3)
//        {
//            Anim.SetTrigger("FinalArmored");
//            enemyData.damage *= damageIncreaseAmout;
//        }
//        else if(hitCount > 1)
//        {
//            Anim.SetTrigger("Armored");
//            enemyData.damage *= damageIncreaseAmout;
//        }*/

//        base.CheckHP();
//    }

//  /*  public override IEnumerator Dead()
//    {
//        chase.speed = 0f;
//        return base.Dead();
//    }*/

   

//    public override void Reset()
//    {
//       /* enemyData.damage = defaultDamage;
//        base.Reset();*/
//    }

//#if UNITY_EDITOR
//    private void OnDrawGizmos()
//    {
//        if (UnityEditor.Selection.activeObject == gameObject)
//        {
//            Gizmos.color = Color.green;
//            Gizmos.DrawWireSphere(transform.position, attackDistance);
//            Gizmos.color = Color.red;
//            Gizmos.DrawWireSphere(transform.position, chaseDistance);
//            Gizmos.color = Color.white;
//        }
//    }

//    public void SetTackle(bool on)
//    {
        
//    }
//#endif
//}
