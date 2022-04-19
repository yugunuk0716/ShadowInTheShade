using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : PoolableMono
{
    private Rigidbody2D rigid;
    public Rigidbody2D Rigid
    {
        get
        {
            if (rigid == null)
                rigid = GetComponent<Rigidbody2D>();
            return rigid;
        }
    }

    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
       
        GameManager.Instance.onPlayerChangeType.AddListener(() =>
        {
            print(anim == null);
            print(PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
            anim.SetBool("isShadow", PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
            

        });
    }

    private void OnEnable()
    {
        anim.SetBool("isShadow", PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
    }
    public override void Reset()
    {

    }

}
