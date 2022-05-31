using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    public bool IsHit { get; set; }
    public int LastHitObjNumber { get; set; }

    public void GetHit(float damage, int objNum);
    void KnockBack(Vector2 direction, float power, float duration);
}
