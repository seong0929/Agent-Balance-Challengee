using UnityEngine;
using QFunction;

public class RLAlgorithm : MonoBehaviour
{
    private RewardSystem rewardSystem;
    private QTable qTable;
    private TiltedGround tiltedGround;
    private AgentController agent;

    private State currentState;
    private Action currentAction;

    private void Awake()
    {
        rewardSystem = GetComponent<RewardSystem>();
        qTable = new QTable(int.MaxValue, 4, 11);
        tiltedGround = GameManager.instance.GetComponent<TiltedGround>();
        agent = GameManager.instance.agent;
        currentState = GetCurrentState();
    }

    private void Update()
    {
        currentState = GetCurrentState();
        currentAction = qTable.GetBestAction(currentState);

        // �ൿ�� ����� ������
        bool success = GetActionResult();

        float reward = rewardSystem.GetReward();
        State nextState = GetCurrentState();

        qTable.UpdateQValue(currentState, currentAction, nextState, reward);
    }

    private bool GetActionResult()
    {
        // �ൿ�� �����ϰ� ����� �޾ƿ�
        bool success = true; // �⺻������ �������� �ʱ�ȭ

        // PerformAction �޼��带 ȣ���Ͽ� �ൿ ����
        agent.GetComponent<AgentController>().PerformAction(currentAction);

        // �ൿ ����� ���� success ���� ����
        // ��: �ൿ ���� �� � ���ǿ� ���� success ���� ������Ʈ

        if (transform.position.y < 0f)
        {
            success = false; // �������� ��� ���з� ����
        }

        return success;
    }

    private State GetCurrentState()
    {
        bool isAlive = true; // ���� ���θ� �����ϴ� ������ ���� �� �Ҵ�
        float groundSlope = GetGroundSlope(); // ���� ���� ���� ���� ������
        return new State(isAlive, groundSlope);
    }

    private float GetGroundSlope()
    {
        float slope = tiltedGround.transform.rotation.eulerAngles.x;

        // ���� ���� -90������ 90�� ���̷� ����
        slope = Mathf.Clamp(slope, -90f, 90f);

        return slope;
    }
}