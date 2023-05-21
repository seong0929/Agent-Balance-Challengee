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

        // 행동의 결과를 가져옴
        bool success = GetActionResult();

        float reward = rewardSystem.GetReward();
        State nextState = GetCurrentState();

        qTable.UpdateQValue(currentState, currentAction, nextState, reward);

        currentState = nextState;
    }

    private bool GetActionResult()
    {
        // 행동을 수행하고 결과를 반환
        // 행동 결과에 따라 성공 여부를 판단
        return true; // 행동 결과에 따라서 true 또는 false로 반환
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