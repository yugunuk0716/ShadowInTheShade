using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashCollider : MonoBehaviour
{
    public bool isDashing = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDashing && (1 << collision.gameObject.layer & LayerMask.GetMask("Enemy")) > 0)
        {
            if (GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Human))
            {
                for (int i = 0; i < 5; i++)
                {
                    GameObject e = PoolManager.Instance.Pop("ShadowEffect").gameObject;
                    e.transform.position = collision.transform.position;
                }
                StartCoroutine(CallonHumanDashCrossEnemy(collision));
            }
        }

    }
    public IEnumerator CallonHumanDashCrossEnemy(Collider2D collision)
    {
        yield return null;
        GameManager.Instance.onHumanDashCrossEnemy.Invoke(collision.gameObject);

    }
}
