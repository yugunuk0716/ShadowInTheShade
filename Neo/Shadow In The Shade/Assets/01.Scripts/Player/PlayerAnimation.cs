using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Vector2 lastMoveDir;
    private PlayerInput playerInput;
    private Vector2 moveDir;
    private Animator playerAnimator;
    public Animator playerTypeChangeEffcetAnimator;
    private GameObject playerSprite;

    private void Start()
    {
        lastMoveDir = Vector2.zero;
        playerAnimator = GetComponent<Animator>();
        playerInput = GameManager.Instance.player.GetComponent<PlayerInput>();
        playerTypeChangeEffcetAnimator = GameObject.Find("PlayerTypeChangeEffectObj").GetComponent<Animator>();
        playerSprite = this.gameObject;
        //GameManager.Instance.onPlayerChangeType.AddListener(() => { StartCoroutine(ChangePlayerTypeAnimation()); });
        GameManager.Instance.onPlayerAttack.AddListener((stack) => { StartCoroutine(PlayerAttackAnimation(stack)); });
        GameManager.Instance.onPlayerDash.AddListener(() => 
        {
            if (GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Human))
            {
                StartCoroutine(PlayerDashAnimation());
            }
        });
    }

    public void StartCoChangePlayerTypeAnimation()
    {
        //Debug.Log("?");
        StartCoroutine(ChangePlayerTypeAnimation());
    }

    private void Update()
    {
        UpdatePlayerAnimation();
    }

    private void UpdatePlayerAnimation()
    {
        moveDir = playerInput.moveDir.normalized;
        playerAnimator.SetFloat("AnimMoveX", moveDir.x);
        playerAnimator.SetFloat("AnimMoveY", moveDir.y);
        playerAnimator.SetFloat("AnimMoveMagnitude", moveDir.magnitude);

        if (moveDir.x == 0 && moveDir.y == 0)
        {
            return;
        }

        SetLastMove();
    }

    public void SetLastMove()
    {
        lastMoveDir = moveDir;
        playerAnimator.SetFloat("AnimLastMoveX", lastMoveDir.x);
        playerAnimator.SetFloat("AnimLastMoveY", lastMoveDir.y);
    }

    public IEnumerator ChangePlayerTypeAnimation()
    {
        PlayerSO so = GameManager.Instance.playerSO;
        yield return null;


        if (so.playerStates.Equals(PlayerStates.Human))
        {
            playerTypeChangeEffcetAnimator.SetBool("ShadowToHuman", true);
            yield return new WaitForSeconds(.2f);
            playerAnimator.SetBool("IsShadow", false);
            yield return new WaitForSeconds(.2f);
        }
        else
        {
            playerTypeChangeEffcetAnimator.SetBool("HumanToShadow", true);
            yield return new WaitForSeconds(.1f);
            playerAnimator.SetBool("IsShadow", true);
            yield return new WaitForSeconds(.9f);
        }

       
        GameManager.Instance.onPlayerTypeChanged?.Invoke();
        GameManager.Instance.playerSO = so;
        playerTypeChangeEffcetAnimator.SetBool("ShadowToHuman", false);
        playerTypeChangeEffcetAnimator.SetBool("HumanToShadow", false);
        so.canChangePlayerType = true;
    }


    public IEnumerator PlayerAttackAnimation(int attackStack)
    {
        playerAnimator.SetBool("IsAttack", true);
        playerAnimator.SetInteger("AttackCount", attackStack);
        yield return new WaitForSeconds((500 - GameManager.Instance.playerSO.attackStats.ASD)/1000);
        playerAnimator.SetBool("IsAttack", false);
        GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
    }

    public IEnumerator PlayerDashAnimation()
    {
        playerAnimator.SetBool("IsDash", true);
        yield return new WaitForSeconds(GameManager.Instance.playerSO.moveStats.DRT);
        playerAnimator.SetBool("IsDash", false);
    }

    public void SetFalse()
    {
        GameManager.Instance.player.gameObject.SetActive(false);
    }


}
