using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    protected Rigidbody2D myRigid;
    protected Transform playerTrm;

    protected State nextState;

    float detectDistance = 10.0f;
    float attackDistance = 7.0f;

    public State(GameObject obj, Rigidbody2D rigid , Animator anim, Transform targetTransform)
    {
        this.myObj = obj;
        this.myAnim = anim;
        this.myRigid = rigid;
        this.playerTrm = targetTransform;

        this.curEvent = eEvent.ENTER;
    }

    public virtual void Enter()
    {
        curEvent = eEvent.UPDATE;
    }

    public virtual void Update()
    {
        curEvent = eEvent.UPDATE;
    }

    public virtual void Exit()
    {
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
        Vector3 dir = playerTrm.position - myObj.transform.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward);

        return dir.magnitude < detectDistance;
    }

    public bool CanAttackPlayer()
    {
        Vector3 dir = playerTrm.position - myObj.transform.position;
        return (dir.magnitude < attackDistance);
    }

    public bool IsPlayerBehind()
    {
        Vector3 dir = myObj.transform.position - playerTrm.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward);

        if (dir.magnitude < 3.0f)
        {
            return true;
        }
        return false;
    }

}

public class Idle : State
{
    public Idle(GameObject obj, Rigidbody2D rigid, Animator anim, Transform targetTransform) : base(obj, rigid, anim, targetTransform)
    {
        stateName = eState.IDLE;
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
            nextState = new Pursue(myObj, myRigid, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else if (Random.Range(0, 5000) < 10)
        {
            nextState = new Patrol(myObj, myRigid, myAnim, playerTrm);
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
public class Patrol : State
{
    public Patrol(GameObject obj, Rigidbody2D rigid, Animator anim, Transform targetTransform) : base(obj, rigid ,anim, targetTransform)
    {
        stateName = eState.PARTROL;
        DamageManager.Instance.Log("패트롤");
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isPatrol");
        base.Enter();
    }

    public override void Update()
    {
        // 추적
        //myAgent.SetDestination(playerTrm.position);
        //if (myAgent.hasPath)
        //{
        //    if (CanAttackPlayer())
        //    {
        //        // nextState = new Attack(myObj, myAgent, myAnim, playerTrm);
        //        curEvent = eEvent.EXIT;
        //    }
        //    else if (!CanSeePlayer())
        //    {
        //        nextState = new Patrol(myObj, myAgent, myAnim, playerTrm);
        //        curEvent = eEvent.EXIT;
        //    }
        //}

    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isPatrol");
        base.Exit();
    }

}


public class Pursue : State
{
    public Pursue(GameObject obj, Rigidbody2D rigid, Animator anim, Transform targetTransform) : base(obj, rigid, anim, targetTransform)
    {
        stateName = eState.PURSUE;
        rigid.velocity = (myObj.transform.position - playerTrm.position).normalized * 5f;
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
             nextState = new Attack(myObj, myRigid, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else if (!CanAttackPlayer())
        {
            nextState = new Patrol(myObj, myRigid, myAnim, playerTrm);
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

    AudioSource shootEffect;

    public Attack(GameObject obj, Rigidbody2D rigid, Animator anim, Transform targetTransform) : base(obj, rigid, anim, targetTransform)
    {
        stateName = eState.ATTACK;
        //shootEffect = obj.GetComponent<AudioSource>();
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
        float angle = Vector3.Angle(dir, myObj.transform.position);

        dir.y = 0;

        myObj.transform.rotation = Quaternion.Slerp(myObj.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);


        if (!CanAttackPlayer())
        {
            nextState = new Patrol(myObj, myRigid, myAnim, playerTrm);
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

    public RunAway(GameObject obj, Rigidbody2D rigid, Animator anim, Transform targetTransform) : base(obj, rigid, anim, targetTransform)
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

