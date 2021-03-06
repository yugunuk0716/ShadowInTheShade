using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
   // [HideInInspector]
    public Vector2 dir;

    private PlayerSO playerSO;

    private void Start()
    {
        //GameManager.Instance.OnPlayerChangeType.AddListener(ChangePlayerType);
        playerSO = GameManager.Instance.currentPlayerSO;
    }

    private void Update()
    {
        if (UIManager.Instance._isPopuped)
            return;


        if(!GameManager.Instance.isAttack)
        {
            dir.x = Input.GetAxisRaw("Horizontal");
            dir.y = Input.GetAxisRaw("Vertical");
            GameManager.Instance._dir = dir;
        }

        

        if(Input.GetButtonDown("ChangePlayerType"))
        {
            if (playerSO.canChangePlayerType)
            {
                GameManager.Instance.OnPlayerChangeType.Invoke();
                playerSO.canChangePlayerType = false;
            }
            else
                print("준비되지 않았습니다");
        }


        if (Input.GetButtonDown("Fire1"))
        {
            if (playerSO.playerStates.Equals(PlayerStates.Human) && dir != Vector2.zero)
                GameManager.Instance.OnPlayerDash.Invoke();
            else if(playerSO.playerStates.Equals(PlayerStates.Shadow))
                GameManager.Instance.OnPlayerAttack.Invoke();
        }
    }

    public void ChangePlayerType()
    {
        PlayerStates ps = playerSO.playerStates;


        if (ps == PlayerStates.Human)
            ps = PlayerStates.Shadow;
        else
            ps = PlayerStates.Human;



        playerSO.playerStates = ps;

        GameManager.Instance.currentPlayerSO = playerSO;
    }
}
