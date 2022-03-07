using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    public void GetHit(int damage);

    void KnockBack(Vector2 direction, float power, float duration);
}
