using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Vector2 lastMoveDir;
    public PlayerMove playerMove;
    public Animator playerTypeChangeEffcetAnimator;
    
    private PlayerInput playerInput;
    private Vector2 moveDir;
    private Vector3 mousePos;
    private Animator playerAnimator;
    private Player player;
    private PlayerWeapon weapon;
    //public Animator playerDashEffcetAnimator;
    private GameObject playerSprite;
    private bool isAttacking = false;

    private float deX;
    private float deY;
    

    private void Start()
    {
        lastMoveDir = Vector2.zero;
        playerAnimator = GetComponent<Animator>();
        player = GameManager.Instance.player.GetComponent<Player>();
        playerInput = GameManager.Instance.player.GetComponent<PlayerInput>();
        playerMove = GameManager.Instance.player.GetComponent<PlayerMove>();
        playerTypeChangeEffcetAnimator = GameObject.Find("PlayerTypeChangeEffectObj").GetComponent<Animator>();
        //playerDashEffcetAnimator = GameObject.Find("PlayerDashEffectObj").GetComponent<Animator>();
        playerSprite = this.gameObject;
        //GameManager.Instance.onPlayerChangeType.AddListener(() => { StartCoroutine(ChangePlayerTypeAnimation()); });
        GameManager.Instance.onPlayerAttack.AddListener((stack) => 
        {
            weapon.dObjData.hitNum += stack + 2;
            StartCoroutine(PlayerAttackAnimation(stack)); 
        });
        GameManager.Instance.onPlayerSkill.AddListener(() => 
        {
            weapon.dObjData.hitNum += 10;
            StartCoroutine(PlayerSkillAnimation()); 
        });
        GameManager.Instance.onPlayerDash.AddListener(() => 
        {
            if (GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Human))
            {
                StartCoroutine(PlayerDashAnimation());
            }
        });

        weapon = GetComponentInChildren<PlayerWeapon>();
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
        //if (playerAnimator.GetBool("IsAttack"))
        //    return;
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
        if(!isAttacking)
        {

            lastMoveDir = moveDir;
            playerAnimator.SetFloat("AnimLastMoveX", lastMoveDir.x);
            playerAnimator.SetFloat("AnimLastMoveY", lastMoveDir.y);

            //if (!playerDashEffcetAnimator.GetBool("isDash"))
            //{
            //    playerDashEffcetAnimator.SetFloat("MoveX", lastMoveDir.x);
            //    playerDashEffcetAnimator.SetFloat("MoveY", lastMoveDir.y);
            //}
            if (!playerAnimator.GetBool("IsDash"))
            {
                deX = lastMoveDir.x;
                deY = lastMoveDir.y;
            }
        }
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
        EndAttack();
    }

    public IEnumerator PlayerSkillAnimation() 
    {
        if (isAttacking)
            yield break;

        float originAnimSpeed = playerAnimator.speed;

        playerAnimator.SetBool("IsSkill", true);
        playerAnimator.speed = GameManager.Instance.playerSO.attackStats.ASD;

        isAttacking = true;
        yield return new WaitUntil(() => !isAttacking);

        playerAnimator.SetBool("IsSkill", false);
        GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
        playerAnimator.speed = originAnimSpeed;
    }


    public IEnumerator PlayerAttackAnimation(int attackStack)
    {
        if (isAttacking || playerInput.isUseSkill)
            yield break;

        float originAnimSpeed = playerAnimator.speed;


        mousePos = (playerInput.mousePos - transform.position).normalized;

        Vector2 deV = Vector2.zero;
        if (GameManager.Instance.playerSO.playerJobState.Equals(PlayerJobState.Berserker))
        {
            if (Mathf.Abs(mousePos.x) > Mathf.Abs(mousePos.y))
            {
                if (mousePos.x < 0)
                {
                    deV = Vector3.left;
                }
                else
                {
                    deV = Vector3.right;
                }
            }
            else
            {
                if (mousePos.y < 0)
                {
                    deV = Vector3.down;
                }
                else
                {
                    deV = Vector3.up;
                }
            }
        }
        if (GameManager.Instance.playerSO.playerJobState.Equals(PlayerJobState.Default))
        {
            float thetha = Quaternion.FromToRotation(Vector3.up, mousePos).eulerAngles.z;


            for (int i = 0; i < 8; i++)
            {
                float under = playerInput.degrees[i] - 22.5f;
                float over = 0f;

                over = playerInput.degrees[i] + 22.5f;

                if (under <= 0)
                {
                    under += 360f;
                    (under, over) = (over, under);
                }



                if (under <= thetha && thetha < over)
                {
                    deV = playerInput.vectors[i];
                    break;
                }

                if (thetha < 45f)
                    deV = playerInput.vectors[2];
            }
        }


        if (Mathf.Abs(mousePos.x) + Mathf.Abs(mousePos.y) == 2)
        {
            GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
            playerAnimator.SetBool("IsAttack", false);
            yield break;
        }
     
        player.isAttack = true;

        isAttacking = true;

        playerAnimator.SetFloat("AnimLastMoveX", deV.x);
        playerAnimator.SetFloat("AnimLastMoveY", deV.y);
        playerAnimator.SetBool("IsAttack", true);
        playerAnimator.SetInteger("AttackCount", attackStack);

        playerAnimator.speed = GameManager.Instance.playerSO.attackStats.ASD;
        playerMove.OnMove(deV, 7f);

        yield return new WaitForSeconds(.15f);

        playerMove.OnMove(deV, 0f);
        //yield return new WaitForSeconds((700 - GameManager.Instance.playerSO.attackStats.ASD) / 1000);
        yield return new WaitUntil(() => !isAttacking);

        playerAnimator.SetBool("IsAttack", false);
        player.isAttack = false;
        GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
        playerAnimator.speed = originAnimSpeed;
    }

    private IEnumerator PlayerDashAnimation()
    {
        playerAnimator.SetBool("IsDash", true);
        yield return new WaitForSeconds(GameManager.Instance.playerSO.moveStats.DRT);
        playerAnimator.SetBool("IsDash", false);
    }

    public void CallShadowDashAnime(int dashPower)
    {
        StartCoroutine(ShadowDashEffectAnimation(dashPower));
    }


    public IEnumerator ShadowDashEffectAnimation(int dashPower)
    {

        if (GameManager.Instance.playerSO.playerStates == PlayerStates.Shadow )
        {
            DashEffectScript playerDashEffcet = PoolManager.Instance.Pop("PlayerDashEffectObj") as DashEffectScript;
            playerDashEffcet.SetClass(GameManager.Instance.playerSO.playerJobState);
            Animator playerDashEffcetAnimator = playerDashEffcet.GetComponent<Animator>();
            if (!playerDashEffcetAnimator.GetBool("isDash"))
            {
                mousePos = (playerInput.mousePos - transform.position).normalized;
                playerDashEffcetAnimator.transform.position = GameManager.Instance.player.position;
                Vector2 deV = Vector2.zero;
                if (GameManager.Instance.playerSO.playerJobState.Equals(PlayerJobState.Default))
                {
                    float thetha = Quaternion.FromToRotation(Vector3.up, mousePos).eulerAngles.z;
                    print(thetha);


                    for (int i = 0; i < 8; i++)
                    {
                        float under = playerInput.degrees[i] - 22.5f;
                        float over = 0f;

                        over = playerInput.degrees[i] + 22.5f;

                        if (under <= 0)
                        {
                            under += 360f;
                            (under, over) = (over, under);
                        }



                        if (under <= thetha && thetha < over)
                        {
                            deV = playerInput.vectors[i];
                            break;
                        }

                        if (thetha < 45f)
                            deV = playerInput.vectors[2];
                    }
                }

                print($"{deV.x},{deV.y}");
                playerDashEffcetAnimator.SetFloat("MoveX", deV.x);
                playerDashEffcetAnimator.SetFloat("MoveY", deV.y);
                print(dashPower);
                playerDashEffcetAnimator.SetInteger("DashPower", dashPower);
                playerDashEffcetAnimator.SetBool("isDash", true);
                yield return new WaitForSeconds(0.6f);
                playerDashEffcetAnimator.SetBool("isDash", false);

               
            }
            playerDashEffcet.gameObject.SetActive(false);
            PoolManager.Instance.Push(playerDashEffcet);

            if (GameManager.Instance.playerSO.playerJobState.Equals(PlayerJobState.Berserker) && dashPower > 0)
            {
                DashEffectScript playerDashEffcet2 = PoolManager.Instance.Pop("PlayerDashEffectObj") as DashEffectScript;
                playerDashEffcet2.SetClass(GameManager.Instance.playerSO.playerJobState);
                Animator playerDashEffcetAnimator2 = playerDashEffcet.GetComponent<Animator>();
                playerDashEffcetAnimator2.transform.position = GameManager.Instance.player.position;
                playerDashEffcetAnimator.SetFloat("MoveX", 0f);
                playerDashEffcetAnimator.SetFloat("MoveY", 0f);
                playerDashEffcetAnimator2.SetInteger("DashPower", dashPower);
                playerDashEffcetAnimator2.SetBool("isDash", true);
                yield return new WaitForSeconds(0.6f);
                playerDashEffcetAnimator2.SetBool("isDash", false);
                playerDashEffcet2.gameObject.SetActive(false);
                PoolManager.Instance.Push(playerDashEffcet2);
            }

        }
    }

    public void SetFalse()
    {
        GameManager.Instance.player.gameObject.SetActive(false);
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

}
