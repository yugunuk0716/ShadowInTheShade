using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTypeChange : MonoBehaviour
{
    private PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GameManager.Instance.player.GetComponent<PlayerInput>();
        GameManager.Instance.onPlayerChangingType.AddListener(ChangeType);
    }

    // Update is called once per frame
/*    void Update()
    {
        if(playerInput.isChangePlayerType)
        {
            if (GameManager.Instance.playerSO.canChangePlayerType)
            {
                PlayerStates _ps = GameManager.Instance.playerSO.playerStates;


                if (_ps == PlayerStates.Human)
                    _ps = PlayerStates.Shadow;
                else
                    _ps = PlayerStates.Human;


                GameManager.Instance.playerSO.playerStates = _ps;
                GameManager.Instance.OnPlayerChangeType.Invoke();
                GameManager.Instance.playerSO.canChangePlayerType = false;
                playerInput.isChangePlayerType = false;
            }
        }
    }*/

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
            GameManager.Instance.onPlayerChangeType?.Invoke();
            GameManager.Instance.playerSO.canChangePlayerType = false;
        }
    }
}
