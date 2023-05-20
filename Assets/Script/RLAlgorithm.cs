using UnityEngine;

public class RLAlgorithm : MonoBehaviour
{
    private RewardSystem rewardSystem;
    private QTable qTable;

    private int currentState;
    private int currentAction;

    private void Awake()
    {
        rewardSystem = GetComponent<RewardSystem>();
        qTable = new QTable(0, 0); // stateCount, actionCount
        currentState = 0;
    }

    private void Update()
    {
        // ���� ���¿��� ������ �ൿ�� ����
        currentAction = qTable.GetBestAction(currentState);
        // ������ �ൿ�� ����

        // �ൿ�� ����� ������
        bool success = GetActionResult();

        // ������ ���
        float reward = rewardSystem.GetReward();

        // ���� ������ ���� �ൿ�� ������Ʈ
        int nextState = GetNextState();

        // Q-Table�� ������Ʈ
        qTable.UpdateQValue(currentState, currentAction, nextState, reward);

        // ���� ���·� ��ȯ
        currentState = nextState;
    }

    private bool GetActionResult()
    {
        // �ൿ�� �����ϰ� ����� ��ȯ
        // �ൿ ����� ���� ���� ���θ� �Ǵ�
        return true; // �ൿ ����� ���� true �Ǵ� false�� ��ȯ
    }

    private int GetNextState()
    {
        // ���� ���¸� �����Ͽ� ��ȯ
        // ���� ���¿� ���� ������ ����
        return 0; // ���� ���¿� �ش��ϴ� ���� ��ȯ
    }
}