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
    private Animator playerAnimator;
    private PlayerWeapon weapon;
    //public Animator playerDashEffcetAnimator;
    private GameObject playerSprite;
    private bool isAttacking = false;
    private Vector3 mousePos;

    private float deX;
    private float deY;
    
    private readonly float[] degrees = new float[] { 270f, 315f, 360f, 45f, 90f, 135f, 180f, 225f };
    //private readonly float[] degrees = new float[] { 90f, 135f, 180f, 225f, 270f, 315f, 0f, 45f };
    private readonly Vector2[] vectors = new Vector2[]
    {
        new Vector2(1f, 0f),
        new Vector2(1f, 1f),
        new Vector2(0f, 1f),
        new Vector2(-1f, 1f),
        new Vector2(-1f, 0f),
        new Vector2(-1f, -1f),
        new Vector2(0f, -1f),
        new Vector2(1f, -1f)
    };
    private void Start()
    {
        lastMoveDir = Vector2.zero;
        playerAnimator = GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.L))
        {
            PrintMousePos();
        }
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


    public IEnumerator PlayerAttackAnimation(int attackStack)
    {
        if (isAttacking)
            yield break;

        float originAnimSpeed = playerAnimator.speed;



        mousePos =(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;



        if(Mathf.Abs(mousePos.x) > Mathf.Abs(mousePos.y))
        {
            if (mousePos.x < 0)
            {
                mousePos = Vector3.left;
            }
            else
            {
                mousePos = Vector3.right;
            }
        }
        else
        {
            if (mousePos.y < 0)
            {
                mousePos = Vector3.down;
            }
            else
            {
                mousePos = Vector3.up;
            }
        }


        Debug.Log(mousePos);

        if (Mathf.Abs(mousePos.x) + Mathf.Abs(mousePos.y) == 2)
        {
            GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
            playerAnimator.SetBool("IsAttack", false);
            yield break;
        }
     

        isAttacking = true;

        playerAnimator.SetFloat("AnimLastMoveX", mousePos.x);
        playerAnimator.SetFloat("AnimLastMoveY", mousePos.y);
        playerAnimator.SetBool("IsAttack", true);
        playerAnimator.SetInteger("AttackCount", attackStack);

        playerAnimator.speed = GameManager.Instance.playerSO.attackStats.ASD;
        playerMove.OnMove(mousePos, 10f);

        yield return new WaitForSeconds(.05f);

        playerMove.OnMove(mousePos, 0f);
        //yield return new WaitForSeconds((700 - GameManager.Instance.playerSO.attackStats.ASD) / 1000);
        yield return new WaitUntil(() => !isAttacking);
/*
        if (attackStack == 0)
        {
        }
        else
        {
            yield return new WaitForSeconds((500 - GameManager.Instance.playerSO.attackStats.ASD) / 1000);
        }*/
        playerAnimator.SetBool("IsAttack", false);
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

    private void PrintMousePos()
    {
        mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float thetha = Quaternion.FromToRotation(Vector3.up, mousePos).eulerAngles.z;

        Vector2 deV = Vector2.right;
        print(thetha);


        for (int i = 0; i < 8; i++)
        {
            //float under = degrees[i] - 22.5f;
            //float over = 0f;

            //if (degrees.Length > i + 1)
            //{
            //    over = degrees[i] + 22.5f;
            //}
            //else
            //{
            //    over = degrees[i] - 22.5f;
            //    under = degrees[i] + 22.5f - 360f;
            //}

            float under = degrees[i] - 22.5f;
            float over = 0f;

            over = degrees[i] + 22.5f;

            if (under <= 0)
            {
                under += 360f;
                (under, over) = (over, under);
            }




            print($"{under} < {thetha} < {over}");
            if (under <= thetha && thetha < over)
            {
                deV = vectors[i];
                break;
            }
        }

        print($"{deV.x},{deV.y}");
        //print(thetha);
        //deX = Mathf.Cos(thetha);
        //deY = Mathf.Sin(thetha);

    }

    public IEnumerator ShadowDashEffectAnimation(int dashPower)
    {

        if (GameManager.Instance.playerSO.playerStates == PlayerStates.Shadow )
        {
            PoolableMono playerDashEffcet = PoolManager.Instance.Pop("PlayerDashEffectObj");
            Animator playerDashEffcetAnimator = playerDashEffcet.GetComponent<Animator>();
            
            if (!playerDashEffcetAnimator.GetBool("isDash"))
            {
                playerDashEffcetAnimator.transform.position = GameManager.Instance.player.position;
                mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                
                //for (int i = 0; i < 8; i++)
                //{
                //    if(i == 0 && 0 <= thetha && thetha < 22.5f)
                //    {
                //        print($"{i} && {i * 45f} && {(i * 45f / 2f)}");
                //        print($"{i * 45f - (i * 45f / 2f)} < {i * 45f + (i * 45f / 2f)}");
                //        thetha = i * 45f * Mathf.Deg2Rad;
                //        break;
                //    }
                //    if (i * 45 - (i * 45 / 2f) <= thetha && thetha < i * 45 + (i * 45 / 2f))
                //    {
                //        print($"{i} && {i * 45f} && {(i * 45f / 2f)}");
                //        print($"{i * 45f - (i * 45f / 2f)} < {i * 45f + (i * 45f / 2f)}");
                //        thetha = i * 45f * Mathf.Deg2Rad;
                //        break;
                //    }
                //}

                float thetha = Quaternion.FromToRotation(Vector3.up, mousePos).eulerAngles.z;
                Vector2 deV = Vector2.zero;
                print(thetha);


                for (int i = 0; i < 8; i++)
                {
                    float under = degrees[i] - 22.5f;
                    float over = 0f;

                    over = degrees[i] + 22.5f;

                    if(under <= 0)
                    {
                        under += 360f;
                        (under, over) = (over, under);
                    }



                    print($"{under} < {thetha} < {over}");
                    if (under <= thetha && thetha < over)
                    {
                        deV = vectors[i];
                        break;
                    }

                    if (thetha < 45f)
                        deV = vectors[2];
                }

                print($"{deV.x},{deV.y}");
                playerDashEffcetAnimator.SetFloat("MoveX", deV.x);
                playerDashEffcetAnimator.SetFloat("MoveY", deV.y);
                playerDashEffcetAnimator.SetInteger("DashPower", dashPower);
                playerDashEffcetAnimator.SetBool("isDash", true);
                yield return new WaitForSeconds(0.6f);
                playerDashEffcetAnimator.SetBool("isDash", false);
            }
            playerDashEffcet.gameObject.SetActive(false);
            PoolManager.Instance.Push(playerDashEffcet);
           
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
