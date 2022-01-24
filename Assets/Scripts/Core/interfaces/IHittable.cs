using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    Vector3 _hitPoint { get; }

    public void GetHit(int damage, GameObject damageDealer);
}
