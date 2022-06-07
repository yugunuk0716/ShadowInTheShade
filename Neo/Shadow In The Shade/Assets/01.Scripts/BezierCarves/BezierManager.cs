using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BezierManager : MonoBehaviour
{
    public float effectCount;
    public GameObject effectObj;

    public GameObject origin;

    public void Start()
    {
        for (int i = 0; i < effectCount; i++)
        {
            GameObject a;
            a = Instantiate(effectObj, origin.transform.position, Quaternion.identity, transform);
            a.SetActive(true);

        }
    }


    /*    public  void SetBezier(GameObject targetObj,Vector3 originPos,Vector3 targetPos,float time ,Ease _ease)
        {
            float value = 0f;
            Vector3 Result = Vector3.zero;
            Vector3 V2 = new Vector3(originPos.x, Random.Range(-4f, 4f));
            Vector3 V3 = new Vector3(targetPos.x, Random.Range(-4f, 4f));

            DOTween.To(() => value, x => x = value, 1f, time).SetEase(_ease);

            StartCoroutine(Lerping(targetObj, originPos, targetPos, V2, V3, Result, value));
        }

        public IEnumerator Lerping(GameObject targetObj, Vector3 originPos, Vector3 targetPos 
            , Vector3 V2, Vector3 V3 , Vector3 Result, float value)
        {
            while(true)
            {
                if(value >= 1f)
                {
                    yield break;
                }
                else
                {
                    yield return null;
                    Vector3 R1 = Vector3.Lerp(originPos, V2, value);
                    Vector3 R2 = Vector3.Lerp(V2, V3, value);
                    Vector3 R3 = Vector3.Lerp(V3, targetPos, value);

                    Vector3 R4 = Vector3.Lerp(R1, R2, value);
                    Vector3 R5 = Vector3.Lerp(R2, R3, value);

                    Result = Vector3.Lerp(R4, R5, value);
                    targetObj.transform.position = Result;
                }
            }
        }*/
}
