using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        yield return null;
        //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * GameManager.Instance.playerSO.moveStats.DSP, ForceMode2D.Impulse);
        yield return new WaitForSeconds(.1f);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
