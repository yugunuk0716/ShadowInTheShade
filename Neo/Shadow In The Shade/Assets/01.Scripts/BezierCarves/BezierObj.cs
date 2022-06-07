using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BezierObj : PoolableMono
{
    public GameObject origin;
    public GameObject target;
    public float value;
    
    private Vector3 VP1;
    private Vector3 VP2;
    private bool isBeziering = false;

    public AnimationCurve ease;

    public void OnEnable()
    {
        value = 0;
        isBeziering = false;
        target = GameManager.Instance.player.gameObject;
        //gameObject.SetActive(false);
    }

    public void Start()
    {
       
        GameManager.Instance.onHumanDashCrossEnemy.AddListener((x) =>
        {
            origin = x;
            transform.position = origin.transform.position;
            isBeziering = true;
            Active();
        });
       
    }

    public void Update()
    {
        if(isBeziering)
        {
            if(value >= 1)
            {
                GameManager.Instance.onPlayerGetShadowOrb?.Invoke();
                Reseting();
            }
            transform.position = DOCurve.CubicBezier.GetPointOnSegment(origin.transform.position, VP1, target.transform.position, VP2, value);
        }
    }

    public void Active()
    {
        DOTween.To(() => value, x => value = x, 1, 1.5f).SetEase(Ease.Linear);
        VP1 = new Vector3(origin.transform.position.x + Random.Range(-15,15), Random.Range(-15, 15), 0);
        VP2 = new Vector3(target.transform.position.x, 0, 0);
    }

    public void Reseting()
    {
        value = 0;
        isBeziering = false;
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {

    }

    /*    public void CallBezier(GameObject g)
        {
            float value = 0;
            while(true)
            {
                DOTween.To(() => value, x => x = value, 1, .5f).SetEase(Ease.Linear).OnComplete(() => Debug.Log(value));

                g.transform.position = DOCurve.CubicBezier.GetSegmentPointCloud(
                  origin.transform.position, new Vector3(origin.transform.position.x, Random.Range(-4, 4), 0),
                  target.transform.position, new Vector3(target.transform.position.y, Random.Range(-4, 4), 0),
                  );

                if(value >= 1f)
                {
                    break;
                }
            }
        }*/



}
