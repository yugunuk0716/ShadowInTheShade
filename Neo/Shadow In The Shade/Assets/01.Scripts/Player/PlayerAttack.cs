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
    private Vector3 mousePos;
    private List<Collider2D> colliderList = new List<Collider2D>();

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
        else if(playerInput.isUseSkill && !GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Attack))
        {
            Skill();
            playerInput.isUseSkill = false;
        }
        if (Time.time > lastAttackTime + attackStackHoldingTime)
        {
            attackStack = false;
        }

    }

    private void Skill()
    {
        if (GameManager.Instance.playerSO.playerJobState.Equals(PlayerJobState.Berserker))
        {
            BerserkerSkill();
        }

        colliderList = colliderList.Distinct().ToList();


        foreach (Collider2D item in colliderList)
        {
            playerWeapon.DisposeDamage(item);
        }
        colliderList.Clear();
        lastAttackTime = Time.time;
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

        mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

      

        if (GameManager.Instance.playerSO.playerJobState.Equals(PlayerJobState.Default))
        {
            DefaultAttack();
        }
        else if (GameManager.Instance.playerSO.playerJobState.Equals(PlayerJobState.Berserker))
        {
            BerserkerAttack();
        }

        colliderList = colliderList.Distinct().ToList();


        foreach (Collider2D item in colliderList)
        {
            playerWeapon.DisposeDamage(item);
        }
        colliderList.Clear();
        lastAttackTime = Time.time;

        SoundManager.Instance.GetAudioSource(attackAudioClip, false, SoundManager.Instance.BaseVolume).Play();
        GameManager.Instance.onPlayerAttack.Invoke(attackStack ? 0 : 1);
    }

    public void EnemyAttackHeal()
    {
        if(GameManager.Instance.playerSO.ectStats.APH != 0)
        {
            GameManager.Instance.player.GetComponent<Player>().CurrHP += GameManager.Instance.playerSO.ectStats.APH;
        }
    }


    public void DefaultAttack()
    {
        Vector3 dir = Vector3.zero;
        if (Mathf.Abs(mousePos.x) > Mathf.Abs(mousePos.y))
        {
            if (mousePos.x < 0)
            {
                dir = Vector3.left;
            }
            else
            {
                dir = Vector3.right;
            }
        }
        else
        {
            if (mousePos.y < 0)
            {
                dir = Vector3.down;
            }
            else
            {
                dir = Vector3.up;
            }
        }

        Vector3 atkSize = attackStack ? new Vector2(1f, 3f) : new Vector2(3f, 1.5f);

        colliderList = Physics2D.OverlapBoxAll(transform.position, Vector2.one , LayerMask.GetMask("Enemy")).ToList();

        Collider2D[] c1 = Physics2D.OverlapBoxAll(transform.position + dir, atkSize * 2f, 0f, LayerMask.GetMask("Enemy"));

        foreach (Collider2D collider in c1)
        {
            colliderList.Add(collider);
        }
    }

    public void BerserkerAttack()
    {

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


        colliderList = Physics2D.OverlapBoxAll(transform.position + overlapXBox, new Vector2(1f, 3f) * 2, 0f, LayerMask.GetMask("Enemy")).ToList();
        Collider2D[] c1 = Physics2D.OverlapBoxAll(transform.position + overlapYBox, new Vector2(2f, 1f) * 3, 0f, LayerMask.GetMask("Enemy"));

        foreach (Collider2D collider in c1)
        {
            colliderList.Add(collider);
        }
    }

    public void BerserkerSkill()
    {
        colliderList = Physics2D.OverlapCircleAll(transform.position, 2f, LayerMask.GetMask("Enemy")).ToList();

        GameManager.Instance.onPlayerSkill.Invoke();
        
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, Vector3.one);
            Gizmos.color = Color.white;

        }
    }
#endif
}
