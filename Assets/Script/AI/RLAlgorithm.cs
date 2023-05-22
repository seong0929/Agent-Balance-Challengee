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

        // 행동의 결과를 가져옴
        bool success = GetActionResult();

        float reward = rewardSystem.GetReward();
        State nextState = GetCurrentState();

        qTable.UpdateQValue(currentState, currentAction, nextState, reward);
    }

    private bool GetActionResult()
    {
        // 행동을 수행하고 결과를 받아옴
        bool success = true; // 기본적으로 성공으로 초기화

        // PerformAction 메서드를 호출하여 행동 수행
        agent.GetComponent<AgentController>().PerformAction(currentAction);

        // 행동 결과에 따라 success 값을 결정
        // 예: 행동 수행 후 어떤 조건에 따라 success 값을 업데이트

        if (transform.position.y < 0f)
        {
            success = false; // 떨어졌을 경우 실패로 설정
        }

        return success;
    }

    private State GetCurrentState()
    {
        bool isAlive = true; // 생존 여부를 설정하는 로직에 따라 값 할당
        float groundSlope = GetGroundSlope(); // 현재 땅의 기울기 값을 가져옴
        return new State(isAlive, groundSlope);
    }

    private float GetGroundSlope()
    {
        float slope = tiltedGround.transform.rotation.eulerAngles.x;

        // 기울기 값을 -90도부터 90도 사이로 제한
        slope = Mathf.Clamp(slope, -90f, 90f);

        return slope;
    }
}