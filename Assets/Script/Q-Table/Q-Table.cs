using UnityEngine;
using QFunction;
using System.Collections.Generic;

public class QTable
{
    private float[,,] qTable;

    public QTable(int stateCount, int actionCount, int slopeCount)
    {
        qTable = new float[stateCount, actionCount, slopeCount];

        // Q-Table�� �ʱⰪ���� ����
        for (int i = 0; i < stateCount; i++)
        {
            for (int j = 0; j < actionCount; j++)
            {
                for (int k = 0; k < slopeCount; k++)
                {
                    qTable[i, j, k] = 0f;
                }
            }
        }
    }

    public Action GetBestAction(State state)
    {
        int bestActionIndex = 0;  // ������ �ൿ �ε����� ������ ����, �ʱⰪ���� 0 ����
        float maxQValue = float.MinValue;  // �ִ� Q-Value�� ������ ����, �ʱⰪ���� ���� ���Ѵ� ����

        for (int actionIndex = 0; actionIndex < qTable.GetLength(1); actionIndex++)
        {
            for (int slopeIndex = 0; slopeIndex < qTable.GetLength(2); slopeIndex++)
            {
                float qValue = qTable[state.isAlive ? 1 : 0, actionIndex, slopeIndex];  // ���� ���¿� �ൿ, ���⿡ ���� Q-Value ��������

                if (qValue > maxQValue)
                {
                    maxQValue = qValue;
                    bestActionIndex = actionIndex;
                }
            }
        }

        return Action.GetActionByIndex(bestActionIndex);  // ������ �ൿ ��ȯ
    }

    public void UpdateQValue(State state, Action action, State nextState, float reward)
    {
        int currentStateIndex = state.isAlive ? 1 : 0;

        int slopeIndex = GetSlopeIndex(state.groundSlope);

        // ���� Q-Value ��������
        float oldQValue = qTable[currentStateIndex, action.Index, slopeIndex];

        // ������ ���� �ൿ�� �ش��ϴ� Q-Value ��������
        float maxNextQValue = GetMaxQValue(nextState);

        // ���ο� Q-Value ���
        float newQValue = CalculateNewQValue(oldQValue, reward, maxNextQValue);

        // Q-Table ������Ʈ
        qTable[currentStateIndex, action.Index, slopeIndex] = newQValue;
    }

    private float GetMaxQValue(State state)
    {
        int stateIndex = state.isAlive ? 1 : 0;
        float maxQValue = float.MinValue;

        // ���� ���¿��� ������ ��� �ൿ �� ���⿡ ���� �ִ� Q-Value ã��
        for (int actionIndex = 0; actionIndex < qTable.GetLength(1); actionIndex++)
        {
            for (int slopeIndex = 0; slopeIndex < qTable.GetLength(2); slopeIndex++)
            {
                float qValue = qTable[stateIndex, actionIndex, slopeIndex];
                if (qValue > maxQValue)
                {
                    maxQValue = qValue;
                }
            }
        }

        return maxQValue;
    }

    private float CalculateNewQValue(float oldQValue, float reward, float maxNextQValue)
    {
        // ���ο� Q-Value ��� (������Ʈ ��Ģ�� ���� ����)
        float learningRate = 0.1f; // �н���
        float discountFactor = 0.9f; // ������

        float newQValue = oldQValue + learningRate * (reward + discountFactor * maxNextQValue - oldQValue);
        return newQValue;
    }

    private int GetSlopeIndex(float slope)
    {
        // ���� ���� �� �ε��� �Ҵ� ����� ������Ʈ�� �°� ����
        int slopeIndex = Mathf.RoundToInt(slope);  // ���⸦ �ݿø��Ͽ� ���� �ε����� ��ȯ

        // �ε����� ������ ����� �ʵ��� ����
        slopeIndex = Mathf.Clamp(slopeIndex, 0, qTable.GetLength(2) - 1);

        return slopeIndex;
    }
}