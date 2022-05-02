using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    public bool IsHit { get; set; }
    public void GetHit(float damage);

    void KnockBack(Vector2 direction, float power, float duration);
}
