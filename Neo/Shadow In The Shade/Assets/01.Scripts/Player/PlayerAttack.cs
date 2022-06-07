using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput playerInput;
   
    private PlayerAnimation playerAnim;
    private bool attackStack;
    private float lastAttackTime = 0f;
    private float attackStackHoldingTime = .8f;

    private AudioClip attackAudioClip;

    private void Start()
    {
        attackStack = true;
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponentInChildren<PlayerAnimation>();
      

        attackAudioClip = Resources.Load<AudioClip>("Sounds/PlayerAttack");
        GameManager.Instance.onEnemyHit.AddListener(EnemyAttackHeal);
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
        if (Time.time > lastAttackTime + attackStackHoldingTime)
        {
            attackStack = false;
        }

    }

    private void Attack()
    {
        GameManager.Instance.playerSO.playerInputState = PlayerInputState.Attack;
        attackStack = !attackStack;

        //Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, .3f, LayerMask.GetMask("Enemy"));


        //foreach (Collider2D col in hits) 
        //{
        //    IDamagable d = col.GetComponent<IDamagable>();

        //    d?.KnockBack(playerAnim.lastMoveDir.normalized, 8f, 0.1f);
        //}

        List<Collider2D> colliderList = new List<Collider2D>();

        Collider2D[] c1 = Physics2D.OverlapBoxAll(transform.position + Vector3.left, new Vector2(1f, 2f), 0f);

        foreach (Collider2D collider in c1)
        {

        }

        c1 = Physics2D.OverlapBoxAll(transform.position + Vector3.down, new Vector2(2f, 1f), 0f);


        lastAttackTime = Time.time;

        SoundManager.Instance.GetAudioSource(attackAudioClip, false, SoundManager.Instance.BaseVolume).Play();
        GameManager.Instance.onPlayerAttack.Invoke(attackStack ?  0 : 1);
    }

    public void EnemyAttackHeal()
    {
        if(GameManager.Instance.playerSO.ectStats.APH != 0)
        {
            GameManager.Instance.player.GetComponent<Player>().CurrHP += GameManager.Instance.playerSO.ectStats.APH;
            UIManager.Instance.SetBar(
                GameManager.Instance.player.GetComponent<Player>().CurrHP / GameManager.Instance.playerSO.ectStats.PMH);
        }
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
