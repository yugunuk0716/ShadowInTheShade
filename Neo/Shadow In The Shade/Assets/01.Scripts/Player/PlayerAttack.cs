using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput playerInput;
    private int attackStack;

    private AudioClip attackAudioClip;

    private void Start()
    {
        attackStack = 0;
        playerInput = GetComponent<PlayerInput>();

        attackAudioClip = Resources.Load<AudioClip>("Sounds/PlayerAttack");
    }

    void Update()
    {
        if(playerInput.isAttack && !GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Attack))
        {
            GameManager.Instance.playerSO.playerInputState = PlayerInputState.Attack;
            Attack();
            playerInput.isAttack = false;
        }
    }

    private void Attack()
    {
        if (attackStack <= 0)
            attackStack++;
        else
            attackStack = 0;
        SoundManager.Instance.GetAudioSource(attackAudioClip, false, SoundManager.Instance.BaseVolume).Play();
        GameManager.Instance.onPlayerAttack.Invoke(attackStack);
    }
}