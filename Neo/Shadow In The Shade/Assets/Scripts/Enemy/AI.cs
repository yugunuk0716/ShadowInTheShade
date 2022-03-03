using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AI : MonoBehaviour
{
    Rigidbody2D myRigid;
    Animator myAnim;

    public Transform playerTrm;

    State curState;

    private void Start()
    {
        myRigid = this.GetComponent<Rigidbody2D>();
        myAnim = this.GetComponent<Animator>();
        playerTrm = GameManager.Instance.player;
        curState = new Idle(this.gameObject, myRigid, myAnim, playerTrm);
    }

    private void Update()
    {
        curState = this.curState.Process();
    }
}
