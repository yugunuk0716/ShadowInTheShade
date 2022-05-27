using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput playerInput;
    private bool attackStack;

    private AudioClip attackAudioClip;

    private void Start()
    {
        attackStack = true;
        playerInput = GetComponent<PlayerInput>();

        attackAudioClip = Resources.Load<AudioClip>("Sounds/PlayerAttack");
    }

    void Update() // 지금 문제 멈춰서 때리면 안나가서 수정했지만 이러면 이제 공격 딜레이가 없음
    {
        if(playerInput.isAttack && !GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Attack))
        {
            if(GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Idle))
            {
                GameManager.Instance.playerSO.playerInputState = PlayerInputState.Attack;
                Attack();
                playerInput.isAttack = false;
            }
            else
            {
                if (Mathf.Abs(playerInput.moveDir.normalized.x) != Mathf.Abs(playerInput.moveDir.normalized.y))
                {
                    GameManager.Instance.playerSO.playerInputState = PlayerInputState.Attack;
                    Attack();
                    playerInput.isAttack = false;
                }
            }
        }
    }

    private void Attack()
    {
        attackStack = !attackStack;
        SoundManager.Instance.GetAudioSource(attackAudioClip, false, SoundManager.Instance.BaseVolume).Play();
        GameManager.Instance.onPlayerAttack.Invoke(attackStack ? 1 : 0);
    }
}
