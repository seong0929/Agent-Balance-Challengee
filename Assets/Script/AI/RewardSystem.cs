using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    private float totalReward;

    private void Start()
    {
        totalReward = 0f;
    }

    public void AddReward(float reward)
    {
        totalReward += reward;
    }

    public float GetReward()
    {
        return totalReward;
    }

    public float CalculateReward(State currentState, Action performedAction, State nextState)
    {
        // 현재 상태에서 다음 상태로 떨어지는 경우 -1 보상
        if (currentState.isAlive && !nextState.isAlive)
        {
            return -1f;
        }

        // 현재 상태와 다음 상태가 땅 위에 있는 경우 0.1 보상
        if (currentState.groundSlope >= 0f && nextState.groundSlope >= 0f)
        {
            return 0.1f;
        }

        // 기본적으로 보상을 0으로 설정
        return 0f;
    }

}
