using UnityEngine;
public class QTable
{
    private float[,] qTable;

    public QTable(int stateCount, int actionCount)
    {
        qTable = new float[stateCount, actionCount];

        // Q-Table을 초기값으로 설정
        for (int i = 0; i < stateCount; i++)
        {
            for (int j = 0; j < actionCount; j++)
            {
                qTable[i, j] = 0f;
            }
        }
    }

    public int GetBestAction(int state)
    {
        // 최적의 행동을 결정하여 반환합니다.
        // Q-Table을 참고하여 최적의 행동을 선택해야 합니다.
        return 0; // 최적의 행동에 해당하는 값을 반환합니다.
    }

    public void UpdateQValue(int state, int action, int nextState, float reward)
    {
        // Q-Table의 값을 업데이트합니다.
        // 업데이트 규칙에 따라 Q-Table 값을 수정해야 합니다.
    }
}
