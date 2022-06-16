using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffectScript : PoolableMono
{

    public List<GameObject> bases = new List<GameObject> ();
    private Animator animator;

    public override void Reset()
    {
        foreach (GameObject item in bases)
        {
            item.transform.localScale = Vector3.one;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator> ();
    }

    void Start()
    {
        GameManager.Instance.onPlayerTypeChanged.AddListener(() =>
        {
            if (GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Human))
            {
                this.gameObject.SetActive(false);
                PoolManager.Instance.Push(this);
            }
        });
    }

    public void SetClass(PlayerJobState job)
    {
        switch (job)
        {
            case PlayerJobState.Default:
                animator.SetBool("isDefault", true);
                animator.SetBool("isBerserker", false);
                animator.SetBool("isArcher", false);
                animator.SetBool("isGreedy", false);
                animator.SetBool("isDev", false);
                break;
            case PlayerJobState.Berserker:
                animator.SetBool("isDefault", false);
                animator.SetBool("isBerserker", true);
                animator.SetBool("isArcher", false);
                animator.SetBool("isGreedy", false);
                animator.SetBool("isDev", false);
                break;
            case PlayerJobState.Archer:
                animator.SetBool("isDefault", false);
                animator.SetBool("isBerserker", false);
                animator.SetBool("isArcher", true);
                animator.SetBool("isGreedy", false);
                animator.SetBool("isDev", false);
                break;
            case PlayerJobState.Greedy:
                animator.SetBool("isDefault", false);
                animator.SetBool("isBerserker", false);
                animator.SetBool("isArcher", false);
                animator.SetBool("isGreedy", true);
                animator.SetBool("isDev", false);
                break;
            case PlayerJobState.Devilish:
                animator.SetBool("isDefault", false);
                animator.SetBool("isBerserker", false);
                animator.SetBool("isArcher", false);
                animator.SetBool("isGreedy", false);
                animator.SetBool("isDev", true);
                break;
        }

    
    }

 
}
