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
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput.isChangePlayerType)
        {
            PlayerStates _ps = GameManager.Instance.playerSO.playerStates;


            if (_ps == PlayerStates.Human)
                _ps = PlayerStates.Shadow;
            else
                _ps = PlayerStates.Human;


            GameManager.Instance.playerSO.playerStates = _ps;
        }
    }
}
