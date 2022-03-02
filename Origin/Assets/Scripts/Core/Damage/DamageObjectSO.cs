using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Core/DamageObjcetSO")]
public class DamageObjectSO : ScriptableObject
{
    [Range(1, 10)] public int _damage = 1;
    [Range(1, 20)] public float _knockBackPower = 5;
    [Range(0.01f, 1f)] public float _knockBackDelay = 0.1f;
}
