using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    private float totalReward;

    private void Start()
    {
        totalReward = 0f;
    }

    public void AddReward(float reward)
    {
        totalReward += reward;
    }

    public float GetReward()
    {
        return totalReward;
    }

    public float CalculateReward(State currentState, Action performedAction, State nextState)
    {
        // ���� ���¿��� ���� ���·� �������� ��� -1 ����
        if (currentState.isAlive && !nextState.isAlive)
        {
            return -1f;
        }

        // ���� ���¿� ���� ���°� �� ���� �ִ� ��� 0.1 ����
        if (currentState.groundSlope >= 0f && nextState.groundSlope >= 0f)
        {
            return 0.1f;
        }

        // �⺻������ ������ 0���� ����
        return 0f;
    }

}
