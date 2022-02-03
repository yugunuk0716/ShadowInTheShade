using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    private EnemyAI _enemyBrain = null;
    [SerializeField]
    private List<AIAction> _actions = null;
    [SerializeField]
    private List<AITransition> _transitions = null;

    private void Awake()
    {
        _enemyBrain = transform.GetComponentInParent<EnemyAI>();
    }

    public void UpdateState()
    {
        //������ �׼ǵ��� ��� ����
        foreach (AIAction action in _actions)
        {
            action.TakeAction();
        }

        //���̰� �̷������ϴ����� �˻�
        foreach (AITransition transition in _transitions)
        {
            // �÷��̾ ��Ÿ��� �ִ°�? => true 

            //�ش� Ʈ�����ǿ� �����ϴ� ��� �������� �����ϰ� �Ѵ�.
            bool result = false;
            foreach (AIDecision decsion in transition.decisions)
            {
                result = decsion.MakeADecision();
                if (!result) break;
            }

            if (result)
            {
                //���̸� �� ���� �ִٸ� �����ض�
                if (transition.positiveResult != null)
                {
                    _enemyBrain.ChangeToState(transition.positiveResult);
                    return;
                }
            }
            else
            {
                //���� ����
                if (transition.negativeResult != null)
                {
                    _enemyBrain.ChangeToState(transition.negativeResult);
                    return;
                }
            }
        }
    }
}