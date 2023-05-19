using UnityEngine;
using TMPro;

public class RLAlgorithm : MonoBehaviour
{
    private RewardSystem rewardSystem;
    private QTable qTable;

    private int currentState;
    private int currentAction;

    public TMP_Text rewardText; // UI Text(TMP) ����

    private void Awake()
    {
        rewardSystem = GetComponent<RewardSystem>();
        qTable = new QTable(0, 0); // stateCount, actionCount
        currentState = 0;

        rewardText = GetComponent<TMP_Text>(); // rewardText �Ҵ�
    }

    private void Update()
    {
        // ���� ���¿��� ������ �ൿ�� �����մϴ�.
        currentAction = qTable.GetBestAction(currentState);
        // ������ �ൿ�� �����մϴ�.

        // �ൿ�� ����� �޾ƿɴϴ�.
        bool success = GetActionResult();

        // ������ ����մϴ�.
        float reward = rewardSystem.GetReward();

        // ���� ������ ���� �ൿ�� ������Ʈ�մϴ�.
        int nextState = GetNextState();

        // Q-Table�� ������Ʈ�մϴ�.
        qTable.UpdateQValue(currentState, currentAction, nextState, reward);

        // ���� ���·� ��ȯ�մϴ�.
        currentState = nextState;

        // ���� ���� UI Text(TMP)�� �Ҵ��Ͽ� ǥ���մϴ�.
        rewardText.text = "Current Reward: " + reward.ToString();
    }

    private bool GetActionResult()
    {
        // �ൿ�� �����ϰ� ����� ��ȯ�մϴ�.
        // �ൿ ����� ���� ���� ���θ� �Ǵ��մϴ�.
        return true; // �ൿ ����� ���� true �Ǵ� false�� ��ȯ�ؾ� �մϴ�.
    }

    private int GetNextState()
    {
        // ���� ���¸� �����Ͽ� ��ȯ�մϴ�.
        // ���� ���¿� ���� ������ �����ؾ� �մϴ�.
        return 0; // ���� ���¿� �ش��ϴ� ���� ��ȯ�ؾ� �մϴ�.
    }
}