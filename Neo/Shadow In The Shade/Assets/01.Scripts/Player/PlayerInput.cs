using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public Vector2 moveDir;
    public Vector2 mouseDir;
    public Vector3 mousePos;
    public bool isDash;
    public bool isAttack;
    public bool isUse;
    public bool isChangePlayerType;
    public bool isHit;
    public bool isDie;
    public bool isUseSkill;

    private Camera mainCam;
    public readonly float[] degrees = new float[] { 270f, 315f, 360f, 45f, 90f, 135f, 180f, 225f };
    public readonly Vector2[] vectors = new Vector2[]
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
        mainCam = Camera.main;
    }


    private void Update()
    {
        if (GameManager.Instance.timeScale <= 0 || isDie)
        {
            moveDir = Vector2.zero;
            isDash = false;
            isAttack = false;
            //  isChangePlayerType = false;
            isUse = false;
            return;
        }

        if (Input.GetButtonDown("Change"))
        {
            print("입력");
        }
        mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition));//.normalized;

        switch (GameManager.Instance.playerSO.playerStates) // 플레이어 타입 상태
        {
            case PlayerStates.Human: // 사람 형태 일때 가능한걸 체크
                switch (GameManager.Instance.playerSO.playerInputState) // 플레이어 입력 체크
                {

                    case PlayerInputState.Dash:
                        if (!EventSystem.current.IsPointerOverGameObject())
                        {
                            isDash = Input.GetButtonDown("Dash");
                        }
                        isChangePlayerType = Input.GetButtonDown("Change");
                        break;
                    case PlayerInputState.Use:
                        isUse = Input.GetButtonDown("Use");
                        break;
                    case PlayerInputState.Idle:
                    case PlayerInputState.Move:
                    case PlayerInputState.Change:
                        moveDir.x = Input.GetAxisRaw("Horizontal");
                        moveDir.y = Input.GetAxisRaw("Vertical");
                        isChangePlayerType = Input.GetButtonDown("Change");
                        if (!EventSystem.current.IsPointerOverGameObject())
                        {
                            isDash = Input.GetButtonDown("Dash");
                        }
                        isUse = Input.GetButtonDown("Use");
                        break;
                }
                break;
            case PlayerStates.Shadow:// 그림자 형태 일때 가능한걸 체크
                switch (GameManager.Instance.playerSO.playerInputState) // 플레이어 입력 체크
                {
                    //case PlayerInputState.Hit:
                    //    isHit = true;
                    //    break;
                    case PlayerInputState.Dash:
                        if (!EventSystem.current.IsPointerOverGameObject())
                        {
                            isDash = Input.GetButtonDown("Dash");
                        }
                        isChangePlayerType = Input.GetButtonDown("Change");
                        break;
                    case PlayerInputState.Attack:
                        /*moveDir = Vector2.zero;
                        break;*/
                    case PlayerInputState.Idle:
                    case PlayerInputState.Move:
                    case PlayerInputState.Change:
                        if (!GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Attack))
                        {
                            isUseSkill = Input.GetButtonDown("Change");
                            isAttack = Input.GetButtonDown("Attack");
                            moveDir.x = Input.GetAxisRaw("Horizontal");
                            moveDir.y = Input.GetAxisRaw("Vertical");
                        }
                        isChangePlayerType = Input.GetButtonDown("Change");
                        if (!EventSystem.current.IsPointerOverGameObject())
                        {
                            isDash = Input.GetButtonDown("Dash");
                        }
                        break;
                }
                break;
        }

    }

    
}
