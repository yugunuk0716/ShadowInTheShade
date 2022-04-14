using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy/EnemyList")]
public class EnemyListSO : ScriptableObject
{
    public List<EnemyDataSO> enemyList;
}
