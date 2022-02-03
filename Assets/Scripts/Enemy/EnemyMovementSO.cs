using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemies/MovementData")]
public class EnemyMovementSO : ScriptableObject
{
    [Range(1, 10)]
    public float maxSpeed = 5;

    [Range(0.1f, 100f)]
    public float aceleration = 50, deAceleration = 50;


}
