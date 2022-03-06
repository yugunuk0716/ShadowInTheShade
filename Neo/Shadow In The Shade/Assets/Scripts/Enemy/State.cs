using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class State
{
    public enum eState // 가질 수 있는 상태 나열
    {
        IDLE, PARTROL, PURSUE, ATTACK, DEAD, RUNAWAY
    }

    public enum eEvent
    {
        ENTER, UPDATE, EXIT
    }

    public eState stateName;

    protected eEvent curEvent;

    protected GameObject myObj;
    protected Animator myAnim;
    protected EnemyAttack myRigid;
    protected Transform playerTrm;
    protected UnityEvent onStateEnter;

    protected State nextState;

    float detectDistance = 5.0f;
    float attackDistance = 3.0f;

    public State(GameObject obj, EnemyAttack attack , Animator anim, Transform targetTransform, UnityEvent unityEvent)
    {
        this.myObj = obj;
        this.myAnim = anim;
        this.myRigid = attack;
        this.playerTrm = targetTransform;

        this.curEvent = eEvent.ENTER;
    }

    public virtual void Enter()
    {
        curEvent = eEvent.UPDATE;
        onStateEnter?.Invoke();
    }

    public virtual void Update()
    {
        curEvent = eEvent.UPDATE;
    }

    public virtual void Exit()
    {
        onStateEnter = null;
        curEvent = eEvent.EXIT;
    }

    public State Process()
    {
        if (curEvent == eEvent.ENTER) Enter();
        if (curEvent == eEvent.UPDATE) Update();
        if (curEvent == eEvent.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }

    public bool CanFindPlayer()
    {
        return Vector2.Distance(playerTrm.position, myObj.transform.position) < detectDistance;
    }

    public bool CanAttackPlayer()
    {
        float dist = Vector2.Distance(playerTrm.position, myObj.transform.position);
        DamageManager.Instance.Log($"{dist}");
        if (dist <= 0.5f)
        {
            return false;
        }
        return Vector2.Distance(playerTrm.position, myObj.transform.position) < attackDistance;
    }

  

}

public class Idle : State
{
    public Idle(GameObject obj, EnemyAttack attack, Animator anim, Transform targetTransform, UnityEvent unityEvent) : base(obj, attack, anim, targetTransform, unityEvent)
    {
        stateName = eState.IDLE;
        attack.rigid.velocity = Vector2.zero;
        DamageManager.Instance.Log("아이들");
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isIdle");
        base.Enter();
    }
    public override void Update()
    {
        if (CanFindPlayer())
        {
            nextState = new Pursue(myObj, myRigid, myAnim, playerTrm, onStateEnter);
            DamageManager.Instance.Log("추격");
            curEvent = eEvent.EXIT;
        }
        else
        {
            base.Update();
        }
    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isIdle");
        base.Exit();
    }

}

public class Pursue : State
{
    public Pursue(GameObject obj, EnemyAttack attack, Animator anim, Transform targetTransform, UnityEvent unityEvent) : base(obj, attack, anim, targetTransform, unityEvent)
    {
        stateName = eState.PURSUE;
        attack.rigid.velocity = (playerTrm.position - myObj.transform.position ).normalized * 10f;
        DamageManager.Instance.Log("추격");
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        // 추적

        if (CanAttackPlayer())
        {
             nextState = new Attack(myObj, myRigid, myAnim, playerTrm, onStateEnter);
            curEvent = eEvent.EXIT;
        }
        else if (!CanAttackPlayer())
        {
            nextState = new Idle(myObj, myRigid, myAnim, playerTrm, onStateEnter);
            curEvent = eEvent.EXIT;
        }


    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isRunning");
        base.Exit();
    }

}

public class Attack : State
{
    float rotationSpeed = 2.0f;
    bool isAttacked = false;

    AudioSource shootEffect;

    public Attack(GameObject obj, EnemyAttack rigid, Animator anim, Transform targetTransform, UnityEvent unityEvent) : base(obj, rigid, anim, targetTransform, unityEvent)
    {
        stateName = eState.ATTACK;
        shootEffect = obj.GetComponent<AudioSource>();
        isAttacked = true;
        DamageManager.Instance.Log("공격");
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isShooting");
        //myAgent.isStopped = true;
        //shootEffect.Play();

        base.Enter();
    }

    public override void Update()
    {

        Vector3 dir = playerTrm.position - myObj.transform.position;
        //float angle = Vector3.Angle(dir, myObj.transform.position);

        dir.y = 0;

        //myObj.transform.rotation = Quaternion.Slerp(myObj.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);


        if (!CanAttackPlayer())
        {
            nextState = new Idle(myObj, myRigid, myAnim, playerTrm, onStateEnter);
            curEvent = eEvent.EXIT;
        }

    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isShooting");
        base.Exit();
    }

}
public class RunAway : State
{
    GameObject safeBox;

    public RunAway(GameObject obj, EnemyAttack attack, Animator anim, Transform targetTransform, UnityEvent unityEvent) : base(obj, attack, anim, targetTransform, unityEvent)
    {
        stateName = eState.RUNAWAY;
        //safeBox = GameObject.FindGameObjectWithTag("SafeBox");
        //myAgent.isStopped = false;
        DamageManager.Instance.Log("도망");
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isRunning");

        // myAgent.isStopped = false;
        // myAgent.speed = 7;
        // myAgent.SetDestination(safeBox.transform.position);

        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isRunning");
        base.Exit();
    }
}

