using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, Vector3 pos)
    {
        transform.position = pos;
        _textMesh.SetText(damageAmount.ToString());


        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(transform.position.y + 0.5f, 1f));
        seq.Join(_textMesh.DOFade(0, 1f));
       

    }

   
}
