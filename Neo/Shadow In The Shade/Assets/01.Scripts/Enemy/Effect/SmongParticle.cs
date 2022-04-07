using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmongParticle : MonoBehaviour
{
    float timer = 0f;

    [System.Obsolete]
    public void FadeOutParticle()
    {
        StartCoroutine(Fade());
    }

    [System.Obsolete]
    IEnumerator Fade()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        Color origin = ps.startColor;
        timer = Time.time;
        float a = 1f;
        while (true)
        {

            a -= 0.004f;
            ps.startColor = new Color(origin.r, origin.g, origin.b, a);
           
            yield return new WaitForSeconds(0.01f);
            if (a <= 0f)
            {
                print(Time.time - timer);
                gameObject.SetActive(false);
                yield break;
            }

        }
    }
}
