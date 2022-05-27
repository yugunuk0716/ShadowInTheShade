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

    void Update() // ���� ���� ���缭 ������ �ȳ����� ���������� �̷��� ���� ���� �����̰� ����
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
