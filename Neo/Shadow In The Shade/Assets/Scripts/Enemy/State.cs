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
    protected Rigidbody2D rigid;
    protected Transform playerTrm;

    protected State nextState;

    float detectDistance = 10.0f;
    float detectAngle = 30.0f;
    float shootDistance = 7.0f;

    public State(GameObject obj, Rigidbody2D rigid , Animator anim, Transform targetTransform)
    {
        this.myObj = obj;
        this.myAnim = anim;
        this.rigid = rigid;
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

    public bool CanSeePlayer()
    {
        Vector3 dir = playerTrm.position - myObj.transform.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward);

        return (dir.magnitude < detectDistance && angle < detectAngle);
    }

    public bool CanAttackPlayer()
    {
        Vector3 dir = playerTrm.position - myObj.transform.position;
        return (dir.magnitude < shootDistance);
    }

    public bool IsPlayerBehind()
    {
        Vector3 dir = myObj.transform.position - playerTrm.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward);

        if (dir.magnitude < 3.0f && angle < detectAngle)
        {
            return true;
        }
        return false;
    }

}

public class Idle: State
{
    public Idle(GameObject obj, Rigidbody2D rigid ,Animator anim, Transform targetTransform) : base(obj, rigid ,anim, targetTransform)
    {
        stateName = eState.IDLE;
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isIdle");
        base.Enter();
    }
    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Pursue(myObj, rigid, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else if (Random.Range(0, 5000) < 10)
        {
            nextState = new Patrol(myObj, rigid ,myAnim, playerTrm);
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

    }

    public override void Enter()
    {
        myAnim.SetTrigger("isRunning");
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
        myAnim.ResetTrigger("isRunning");
        base.Exit();
    }

}

