using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamagePopup : PoolableMono
{
    [SerializeField]
    private TextMeshPro _tmp;
    public int _normalTextSize = 5;
    public int _criticalTextSize = 7;

    private void Start()
    {
        _tmp = GetComponent<TextMeshPro>();
        _tmp.fontSize = _normalTextSize;
    }

    public void SetText(int damageAmount, Vector3 pos, bool isCritical)
    {

        transform.position = new Vector3(pos.x, pos.y, -2f);

        _tmp.SetText(damageAmount.ToString());

        if (isCritical)
        {
            _tmp.color = Color.red;
            _tmp.fontSize = _criticalTextSize;
        }

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(transform.position.y + 0.5f, 1f));
        seq.Join(_tmp.DOFade(0, 1f));
        seq.AppendCallback(() =>
        {
            PoolManager.Instance.Push(this);
        });
    }

  

    public override void Reset()
    {

        _tmp.color = Color.white;
        _tmp.fontSize = _normalTextSize;
        this.gameObject.SetActive(false);
    }
}
