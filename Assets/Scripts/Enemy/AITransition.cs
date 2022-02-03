using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    //Ʈ�������� ���� �������� ���ǵ��� ����
    [field: SerializeField]
    public List<AIDecision> decisions { get; set; }

    //��� ������ �����ϸ� positive
    [field: SerializeField]
    public AIState positiveResult { get; set; }

    //������ �ϳ��� �������� ������ negative
    [field: SerializeField]
    public AIState negativeResult { get; set; }

    private void Awake()
    {
        decisions.Clear();
        GetComponents<AIDecision>(decisions);
    }
}
