using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy/SlimeSO")]
public class SlimeSO : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;

    [field: SerializeField]
    public int MaxHealth { get; set; } = 3;
    [field: SerializeField]
    public int Damage { get; set; } = 1;
    [field: SerializeField]
    public float HitDelay { get; set; } = 0.1f;
}
