using UnityEngine;
using TMPro;

public class RLAlgorithm : MonoBehaviour
{
    private RewardSystem rewardSystem;
    private QTable qTable;

    private int currentState;
    private int currentAction;

    public TMP_Text rewardText; // UI Text(TMP) 참조

    private void Awake()
    {
        rewardSystem = GetComponent<RewardSystem>();
        qTable = new QTable(0, 0); // stateCount, actionCount
        currentState = 0;

        rewardText = GetComponent<TMP_Text>(); // rewardText 할당
    }

    private void Update()
    {
        // 현재 상태에서 최적의 행동을 선택합니다.
        currentAction = qTable.GetBestAction(currentState);
        // 선택한 행동을 수행합니다.

        // 행동의 결과를 받아옵니다.
        bool success = GetActionResult();

        // 보상을 계산합니다.
        float reward = rewardSystem.GetReward();

        // 다음 상태의 최적 행동을 업데이트합니다.
        int nextState = GetNextState();

        // Q-Table을 업데이트합니다.
        qTable.UpdateQValue(currentState, currentAction, nextState, reward);

        // 다음 상태로 전환합니다.
        currentState = nextState;

        // 보상 값을 UI Text(TMP)에 할당하여 표시합니다.
        rewardText.text = "Current Reward: " + reward.ToString();
    }

    private bool GetActionResult()
    {
        // 행동을 수행하고 결과를 반환합니다.
        // 행동 결과에 따라 성공 여부를 판단합니다.
        return true; // 행동 결과에 따라서 true 또는 false로 반환해야 합니다.
    }

    private int GetNextState()
    {
        // 다음 상태를 결정하여 반환합니다.
        // 다음 상태에 대한 로직을 구현해야 합니다.
        return 0; // 다음 상태에 해당하는 값을 반환해야 합니다.
    }
}