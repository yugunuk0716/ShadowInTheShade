using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Mucus : MonoBehaviour, IState
{
    public float slowAmount = 3f;
    public float attachTime = 2f;
    private bool isStateEnter = false;

    int originLayer;
    readonly int targetLayer = 9;


    private Vector2 attachPosition = new Vector2(0f, -0.45f);

    Slime_Mucus mucus;

    public void OnEnter()
    {

        if (mucus == null)
            mucus = GetComponent<Slime_Mucus>();

        originLayer = mucus.gameObject.layer;
        mucus.gameObject.layer = targetLayer;

        
        mucus.SetMucus(true);
        StartCoroutine(LerpRoutine());
        //EffectManager.Instance.BloodEffect(EffectType.SLIME, 0.5f, slowAmount, 0.7f);
      
    }

    public void OnEnd()
    {
        mucus.gameObject.layer = originLayer;
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
        transform.SetParent(null);
        Vector3 randDir = new Vector3(Random.Range(1f, 2f), Random.Range(1f, 2f));
        int idx = Random.Range(0, 2);
        if ( idx == 0)
        {
            randDir *= -1f;
        }
        transform.position = transform.position + randDir;
        mucus.SetMucus(false);
        isStateEnter = false;

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


