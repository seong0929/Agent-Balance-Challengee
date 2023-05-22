using UnityEngine;
using QFunction;
using System.Collections.Generic;

public class QTable
{
    private float[,,] qTable;

    public QTable(int stateCount, int actionCount, int slopeCount)
    {
        qTable = new float[stateCount, actionCount, slopeCount];

        // Q-Table을 초기값으로 설정
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
        int bestActionIndex = 0;  // 최적의 행동 인덱스를 저장할 변수, 초기값으로 0 설정
        float maxQValue = float.MinValue;  // 최대 Q-Value를 저장할 변수, 초기값으로 음의 무한대 설정

        for (int actionIndex = 0; actionIndex < qTable.GetLength(1); actionIndex++)
        {
            for (int slopeIndex = 0; slopeIndex < qTable.GetLength(2); slopeIndex++)
            {
                float qValue = qTable[state.isAlive ? 1 : 0, actionIndex, slopeIndex];  // 현재 상태와 행동, 기울기에 대한 Q-Value 가져오기

                if (qValue > maxQValue)
                {
                    maxQValue = qValue;
                    bestActionIndex = actionIndex;
                }
            }
        }

        return Action.GetActionByIndex(bestActionIndex);  // 최적의 행동 반환
    }

    public void UpdateQValue(State state, Action action, State nextState, float reward)
    {
        int currentStateIndex = state.isAlive ? 1 : 0;

        int slopeIndex = GetSlopeIndex(state.groundSlope);

        // 이전 Q-Value 가져오기
        float oldQValue = qTable[currentStateIndex, action.Index, slopeIndex];

        // 최적의 다음 행동에 해당하는 Q-Value 가져오기
        float maxNextQValue = GetMaxQValue(nextState);

        // 새로운 Q-Value 계산
        float newQValue = CalculateNewQValue(oldQValue, reward, maxNextQValue);

        // Q-Table 업데이트
        qTable[currentStateIndex, action.Index, slopeIndex] = newQValue;
    }

    private float GetMaxQValue(State state)
    {
        int stateIndex = state.isAlive ? 1 : 0;
        float maxQValue = float.MinValue;

        // 현재 상태에서 가능한 모든 행동 및 기울기에 대해 최대 Q-Value 찾기
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
        // 새로운 Q-Value 계산 (업데이트 규칙에 따라 구현)
        float learningRate = 0.1f; // 학습률
        float discountFactor = 0.9f; // 감가율

        float newQValue = oldQValue + learningRate * (reward + discountFactor * maxNextQValue - oldQValue);
        return newQValue;
    }

    private int GetSlopeIndex(float slope)
    {
        // 기울기 범위 및 인덱스 할당 방식은 프로젝트에 맞게 조정
        int slopeIndex = Mathf.RoundToInt(slope);  // 기울기를 반올림하여 정수 인덱스로 변환

        // 인덱스가 범위를 벗어나지 않도록 조정
        slopeIndex = Mathf.Clamp(slopeIndex, 0, qTable.GetLength(2) - 1);

        return slopeIndex;
    }
}