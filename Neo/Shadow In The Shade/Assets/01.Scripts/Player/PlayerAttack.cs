using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerWeapon playerWeapon;
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
        playerWeapon = GetComponentInChildren<PlayerWeapon>();
      

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

        
        // transform.position + vector3.right || transform.position + vector3.down    RightAttack1
        // transform.position + vector3.right || transform.position + vector3.up      RightAttack2
        // tramsform.position + vector3.left  || transform.position + vector3.down    DownAttack1
        // tramsform.position + vector3.right || transform.position + vector3.down    DownAttack2
        // tramsform.position + vector3.left  || transform.position + vector3.up      LeftAttack1
        // tramsform.position + vector3.left  || transform.position + vector3.down    LeftAttack2
        // tramsform.position + vector3.right || transform.position + vector3.up      UpAttack1
        // tramsform.position + vector3.left  || transform.position + vector3.up      UpAttack2

        Vector3 mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        Vector3 overlapXBox = Vector3.zero;
        Vector3 overlapYBox = Vector3.zero;

        if (Mathf.Abs(mousePos.x) > Mathf.Abs(mousePos.y))
        {
            if (mousePos.x < 0)
            {
                overlapXBox = Vector3.left;
                overlapYBox = attackStack ? Vector3.up : Vector3.down;
            }
            else
            {
                overlapXBox = Vector3.right;
                overlapYBox = attackStack ? Vector3.down : Vector3.up;
            }
        }
        else
        {
            if (mousePos.y < 0)
            {
                overlapXBox = attackStack ? Vector3.left : Vector3.right;
                overlapYBox = Vector3.down;
            }
            else
            {
                overlapXBox = attackStack ? Vector3.right : Vector3.left;
                overlapYBox = Vector3.up;
            }
        }


        List<Collider2D> colliderList = Physics2D.OverlapBoxAll(transform.position + overlapXBox , new Vector2(1f, 3f) * 2, 0f, LayerMask.GetMask("Enemy")).ToList();
        Collider2D[] c1 = Physics2D.OverlapBoxAll(transform.position + overlapYBox, new Vector2(2f, 1f) * 3, 0f, LayerMask.GetMask("Enemy"));

        foreach (Collider2D collider in c1)
        {
            colliderList.Add(collider);
        }

        colliderList = colliderList.Distinct().ToList();


        foreach (Collider2D item in colliderList)
        {
            playerWeapon.DisposeDamage(item);
        }

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
            Gizmos.color = Color.red;
            Vector3 mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

            Vector3 overlapXBox = Vector3.zero;
            Vector3 overlapYBox = Vector3.zero;

            if (Mathf.Abs(mousePos.x) > Mathf.Abs(mousePos.y))
            {
                if (mousePos.x < 0)
                {
                    overlapXBox = Vector3.left;
                    overlapYBox = attackStack ? Vector3.up : Vector3.down;
                }
                else
                {
                    overlapXBox = Vector3.right;
                    overlapYBox = attackStack ? Vector3.down : Vector3.up;
                }
            }
            else
            {
                if (mousePos.y < 0)
                {
                    overlapXBox = attackStack ? Vector3.left : Vector3.right;
                    overlapYBox = Vector3.down;
                }
                else
                {
                    overlapXBox = attackStack ? Vector3.right : Vector3.left;
                    overlapYBox = Vector3.up;
                }
            }
            Gizmos.DrawWireCube(transform.position + overlapXBox, new Vector2(1f, 2f) * 3);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + overlapYBox, new Vector2(2f, 1f) * 3);
            Gizmos.color = Color.white;

        }
    }
#endif
}
