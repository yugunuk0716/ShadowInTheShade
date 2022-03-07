using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AI : MonoBehaviour
{
    Animator myAnim;

    public Rigidbody2D myRigid;
    public EnemyAttack myAttack;
    public Transform playerTrm;
    public UnityEvent onStateEnter;

    State curState;

    private void Start()
    {
        myRigid = this.GetComponent<Rigidbody2D>();
        myAttack = GetComponent<EnemyAttack>();
        myAnim = this.GetComponent<Animator>();
        playerTrm = GameManager.Instance.player;
        myAttack.rigid = myRigid;
        curState = new Idle(this.gameObject, myAttack, myAnim, playerTrm, onStateEnter);
    }

    private void Update()
    {
        curState = this.curState.Process();
    }
}
