using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemies/EnemySO")]
public class EnemySO : ScriptableObject
{
    public string _enemyName;
    public GameObject _prefab;

    [field:SerializeField]
    public int MaxHealth { get; set; } = 3;
    [field:SerializeField]
    public int Damage { get; set; } = 1;
    [field:SerializeField]
    public int HitDelay { get; set; } = 1;
}
