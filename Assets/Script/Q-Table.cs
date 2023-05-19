using UnityEngine;
public class QTable
{
    private float[,] qTable;

    public QTable(int stateCount, int actionCount)
    {
        qTable = new float[stateCount, actionCount];

        // Q-Table�� �ʱⰪ���� ����
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
        // ������ �ൿ�� �����Ͽ� ��ȯ�մϴ�.
        // Q-Table�� �����Ͽ� ������ �ൿ�� �����ؾ� �մϴ�.
        return 0; // ������ �ൿ�� �ش��ϴ� ���� ��ȯ�մϴ�.
    }

    public void UpdateQValue(int state, int action, int nextState, float reward)
    {
        // Q-Table�� ���� ������Ʈ�մϴ�.
        // ������Ʈ ��Ģ�� ���� Q-Table ���� �����ؾ� �մϴ�.
    }
}
