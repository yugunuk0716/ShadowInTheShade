using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Core/DamagableObjectSO")]
public class DamagableObjectSO : ScriptableObject
{
    [Range(0, 10)] public float damage = 1;
    [Range(1, 20)] public float knockBackPower = 5;
    [Range(0.01f, 1f)] public float knockBackDelay = 0.1f;
}
