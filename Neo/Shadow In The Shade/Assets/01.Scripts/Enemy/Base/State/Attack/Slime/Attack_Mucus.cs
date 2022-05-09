using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Mucus : MonoBehaviour, IState
{
    public float slowAmount = 3f;
    public float attachTime = 2f;
    public bool isStateEnter = false;

    int originLayer;

    private RaycastHit2D hit2D;
    [SerializeField]
    private LayerMask whatIsWall;
    private Vector2 attachPosition = new Vector2(0f, -0.45f);

    private readonly int targetLayer = 9;


    Slime_Mucus mucus;

    public void OnEnter()
    {

        if (mucus == null)
            mucus = GetComponent<Slime_Mucus>();

        if(whatIsWall == default(LayerMask))
        {
            whatIsWall = LayerMask.GetMask("Wall");
        }

        originLayer = mucus.gameObject.layer;
        mucus.gameObject.layer = targetLayer;

        
        mucus.SetMucus(true);
        StartCoroutine(LerpRoutine());
        //EffectManager.Instance.BloodEffect(EffectType.SLIME, 0.5f, slowAmount, 0.7f);
      
    }

    public void OnEnd()
    {
       
    }

    private void Update()
    {
        if (!isStateEnter)
            return;
        transform.localPosition = attachPosition;
        
    }

    IEnumerator AttackRoutine()
    {
       
        transform.SetParent(GameManager.Instance.player);
        float spd = GameManager.Instance.playerSO.moveStats.SPD;
        GameManager.Instance.playerSO.moveStats.SPD = Mathf.Clamp(GameManager.Instance.playerSO.moveStats.SPD - slowAmount, 0, spd);
        yield return new WaitForSeconds(attachTime);
        GameManager.Instance.playerSO.moveStats.SPD += slowAmount;
        transform.SetParent(PoolManager.Instance.transform);
        transform.position = GameManager.Instance.player.position;
        mucus.gameObject.layer = originLayer;

        Vector3 randDir = new Vector3(Random.Range(1f, 1.5f), Random.Range(1f, 1.5f));
        Vector3 origin = transform.position;
        int idx = Random.Range(0, 2);
        if (idx == 0)
        {
            randDir *= -1f;
        }

        hit2D = Physics2D.Raycast(transform.position, origin + randDir, 2f, LayerMask.GetMask("Wall"));
        if (hit2D.collider == null)
        {
            randDir *= -1;
        }

        print("?");
        //transform.position = transform.position + randDir;
        print(origin + randDir);
        float t = 0f;
        while (true)
        {
            t += Time.deltaTime;
            if (t >= 0.9f)
            {
                mucus.SetMucus(false);
                isStateEnter = false;
                transform.position = Vector3.Lerp(origin, origin + randDir, 1);
                yield break;
            }
            transform.position = Vector3.Lerp(origin, origin + randDir, t);

            yield return null;
        }



    }

    IEnumerator LerpRoutine()
    {
        float t = 0f;
        while (true)
        {
            t += Time.deltaTime;
            if(t >= 0.9f)
            {
                StartCoroutine(AttackRoutine());
                isStateEnter = true;
                yield break;
            }
            transform.position = Vector3.Lerp(transform.position, GameManager.Instance.player.position + (Vector3)attachPosition, t);

            yield return null;
        }
    }
}


