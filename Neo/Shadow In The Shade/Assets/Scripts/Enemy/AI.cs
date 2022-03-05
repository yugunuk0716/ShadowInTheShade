using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AI : MonoBehaviour
{
    Rigidbody2D myRigid;
    Animator myAnim;

    public Transform playerTrm;
    public UnityEvent onStateEnter;

    State curState;

    private void Start()
    {
        myRigid = this.GetComponent<Rigidbody2D>();
        myAnim = this.GetComponent<Animator>();
        playerTrm = GameManager.Instance.player;
        curState = new Idle(this.gameObject, myRigid, myAnim, playerTrm, onStateEnter);
    }

    private void Update()
    {
        curState = this.curState.Process();
    }
}
