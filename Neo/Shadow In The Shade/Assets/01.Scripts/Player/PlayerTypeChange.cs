using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTypeChange : MonoBehaviour
{
    public PlayerAnimation playerAnim;
    private PlayerInput playerInput;
   
    void Start()
    {
        playerInput = GameManager.Instance.player.GetComponent<PlayerInput>();
        playerAnim = GameManager.Instance.player.GetComponentInChildren<PlayerAnimation>();
        GameManager.Instance.onPlayerChangeType.AddListener(ChangeType);
    }

    void Update()
    {
        #region ¡÷ºÆ
        //if (playerInput.isChangePlayerType)
        //{
        //    if (GameManager.Instance.playerSO.canChangePlayerType)
        //    {
        //        PlayerStates _ps = GameManager.Instance.playerSO.playerStates;


        //        if (_ps == PlayerStates.Human)
        //            _ps = PlayerStates.Shadow;
        //        else
        //            _ps = PlayerStates.Human;


        //        GameManager.Instance.playerSO.playerStates = _ps;
        //        GameManager.Instance.onPlayerChangeType.Invoke();
        //        GameManager.Instance.playerSO.canChangePlayerType = false;
        //        playerInput.isChangePlayerType = false;
        //    }
        //}
        #endregion



    }

    public void ChangeType()
    {
        if (GameManager.Instance.playerSO.canChangePlayerType)
        {
            PlayerStates _ps = GameManager.Instance.playerSO.playerStates;


            if (_ps == PlayerStates.Human)
                _ps = PlayerStates.Shadow;
            else
                _ps = PlayerStates.Human;


            GameManager.Instance.playerSO.playerStates = _ps;
            GameManager.Instance.player.GetComponentInChildren<PlayerAnimation>().StartCoChangePlayerTypeAnimation();
           // GameManager.Instance.onPlayerChangeType?.Invoke();
            GameManager.Instance.playerSO.canChangePlayerType = false;
        }
    }
}
