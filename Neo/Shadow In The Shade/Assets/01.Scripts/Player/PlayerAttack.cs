using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerAnimation playerAnim;
    private bool attackStack;

    private AudioClip attackAudioClip;

    private void Start()
    {
        attackStack = true;
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponentInChildren<PlayerAnimation>();

        attackAudioClip = Resources.Load<AudioClip>("Sounds/PlayerAttack");
    }

    void Update()
    {
        if(playerInput.isAttack && !GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Attack))
        {
            if(GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Idle))
            {
                Attack();
                playerInput.isAttack = false;
            }
            else
            {
                if (Mathf.Abs(playerInput.moveDir.normalized.x) != Mathf.Abs(playerInput.moveDir.normalized.y))
                {
                    Attack();
                    playerInput.isAttack = false;
                }
            }
        }
    }

    private void Attack()
    {
        GameManager.Instance.playerSO.playerInputState = PlayerInputState.Attack;
        attackStack = !attackStack;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, .3f, LayerMask.GetMask("Enemy"));


        foreach (Collider2D col in hits) 
        {
            IDamagable d = col.GetComponent<IDamagable>();

            print(playerAnim.lastMoveDir.normalized);
            d?.KnockBack(playerAnim.lastMoveDir.normalized, 8f, 0.1f);
        }

        SoundManager.Instance.GetAudioSource(attackAudioClip, false, SoundManager.Instance.BaseVolume).Play();
        GameManager.Instance.onPlayerAttack.Invoke(attackStack ? 1 : 0);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, .3f);
            Gizmos.color = Color.white;
        }
    }
#endif
}
