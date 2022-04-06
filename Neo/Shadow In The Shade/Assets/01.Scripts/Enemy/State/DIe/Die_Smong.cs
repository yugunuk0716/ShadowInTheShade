using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Die_Smong : MonoBehaviour, IState
{
    private GameObject dieParticle;
    GameObject obj;

    [System.Obsolete]
    public void OnEnter()
    {
        if (dieParticle == null)
            dieParticle = Resources.Load<GameObject>("Smong Die Effect");

        obj = Instantiate(dieParticle);
        print(this.transform.position);
        obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, - 5f);

        if (obj != null)
        {
            obj.GetComponent<SmongParticle>().FadeOutParticle();
        }
    }

    public void OnEnd()
    {
       
    }

   

}
