using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementData : MonoBehaviour
{

    [field: SerializeField]
    public Vector2 direction { get; set; }

    [field: SerializeField]
    public Vector2 pointOfInterest { get; set; }
    //������ �ִ� ����. ���������� �������� ����� �����ϰ� ����� ���

}
