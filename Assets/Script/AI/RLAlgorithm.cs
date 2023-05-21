using UnityEngine;

public class RLAlgorithm : MonoBehaviour
{
    private RewardSystem rewardSystem;
    private QTable qTable;
    private TiltedGround tiltedGround;

    private State currentState;
    private Action currentAction;

    private void Awake()
    {
        rewardSystem = GetComponent<RewardSystem>();
        qTable = new QTable(int.MaxValue, 4, 11);
        tiltedGround = GetComponent<TiltedGround>();
        currentState = GetCurrentState();
    }

    private void Update()
    {
        currentAction = qTable.GetBestAction(currentState);

        // �ൿ�� ����� ������
        bool success = GetActionResult();

        float reward = rewardSystem.GetReward();
        State nextState = GetCurrentState();

        qTable.UpdateQValue(currentState, currentAction, nextState, reward);

        currentState = nextState;
    }

    private bool GetActionResult()
    {
        // �ൿ�� �����ϰ� ����� ��ȯ
        // �ൿ ����� ���� ���� ���θ� �Ǵ�
        return true; // �ൿ ����� ���� true �Ǵ� false�� ��ȯ
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